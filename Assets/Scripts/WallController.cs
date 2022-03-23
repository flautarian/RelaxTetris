using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    public enum INITIAL_POSITION{
        TOP, BOTTOM, FRONT, BACK
    }
    public float rotation =0;
    public float scale =0;
    public Color cubesColor = Color.white;

    [SerializeField]
    private float wallVelocity;
    public float wallVelocityCompl;

    private bool touchedObjective = false, correctTouchedPiece = false;

    private Rigidbody rb;

    private PieceController pController;

    private Animator animator;

    [SerializeField]

    internal INITIAL_POSITION initialPosition;

    [SerializeField] internal WallGeneratorController wallGeneratorController;

    [SerializeField] internal WallConfig wallConfig;

    private void Awake() {
        GetWallModelFromConstants();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    public void ActivateGameObjectChilds() {
        ActivateGameObjectChildsRec(transform, true);
    }

    private void OnEnable() {
        touchedObjective = false;
    }

    private void ActivateGameObjectChildsRec(Transform t, bool state){
        t.gameObject.SetActive(state);
        if(t.childCount > 0){
            for (int i = 0; i < t.childCount; i++){
                var obj = t.GetChild(i);
                ActivateGameObjectChildsRec(obj, state);
            }
        }
    }

    private void GetWallModelFromConstants(){
        if(wallGeneratorController != null){
            wallConfig = wallGeneratorController.availableWalls[Random.Range(0, wallGeneratorController.availableWalls.Length)];
        }
    }

    internal void TouchWallWithPiece(PieceController piece){
        pController = piece;
    }

    private void FinishPieceTravel(){
        //Debug.LogFormat("wall x: {0}, y: {1}, z: {2}", wallConfig.SuccessfulRotation.x, wallConfig.SuccessfulRotation.y, wallConfig.SuccessfulRotation.z);
        //Debug.LogFormat("player x: {0}, y: {1}, z: {2}", x, y, z);
        if(pController != null){
            transform.position = new Vector3(0, 0, 0);
            correctTouchedPiece = pController.gameObject.tag.Equals(wallConfig.PieceTag) && wallConfig.SuccessfulRotation.Equals(new Vector3(pController.xOrientation,pController.yOrientation,pController.zOrientation));
            animator.SetTrigger(correctTouchedPiece ? "success" : "failed");
            if(!correctTouchedPiece)
                pController.KillPlayer();
            wallVelocityCompl =0;
            touchedObjective = true;
        }
    }
    public void FailColisionPixelExplodes(){
        //TODO: impl single rb to echa available pixel and add force impulse from center
    }

    public void DisposeWall(){
        GameManager.Instance.ReturnToPool(this.name, this.gameObject);
    }

    private void FixedUpdate() {
        if(!touchedObjective){
            switch(initialPosition){
                case INITIAL_POSITION.TOP:
                    rb.velocity = new Vector3(0, (wallVelocity + wallVelocityCompl) * -1, 0);
                break;

                case INITIAL_POSITION.BOTTOM:
                    rb.velocity = new Vector3(0, (wallVelocity + wallVelocityCompl), 0);
                break;

                case INITIAL_POSITION.FRONT:
                    rb.velocity = new Vector3((wallVelocity + wallVelocityCompl) * -1, 0, 0);
                break;

                case INITIAL_POSITION.BACK:
                    rb.velocity = new Vector3((wallVelocity + wallVelocityCompl), 0, 0);
                break;
            }
        }
        else rb.velocity = new Vector3(0, 0, 0);
    }

    private void Update() {
        if(Vector3.Distance(Vector3.zero, transform.position) < 1f && !touchedObjective)
            FinishPieceTravel();
    }

    
    
    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0) && !touchedObjective){
            animator.SetTrigger("impulsed");
        }
    }
}
