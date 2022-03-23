using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceController : MonoBehaviour
{
    private Ray ray;
    private Vector3 newPoint = Vector3.zero;

    internal int xOrientation =0;
    internal int yOrientation =0;
    internal int zOrientation =0;
    
    [SerializeField]
    private Vector3 objRotation;
        
    [SerializeField]
    private Transform pieceRotator;

    internal PlayerController playerController;

    public static event Action<int> OnComunicateStatusPlayer = delegate { };

    
    void Start()
    {
        objRotation = Vector3.zero;
        pieceRotator = transform.parent;
        RotationButtonController.OnChangeRotation += RotatePiece;
    }

    private void OnDestroy() {
        RotationButtonController.OnChangeRotation -= RotatePiece;
    }

    // Update is called once per frame
    void Update()
    {
        /*ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
            if("plane".Equals(hit.transform.tag)){
                newPoint = Vector3.ClampMagnitude(hit.point, 10);
                newPoint.y = pieceRotator.parent.position.y;
                pieceRotator.position = Vector3.Lerp(pieceRotator.position, newPoint, Time.deltaTime * 10);
            }
        }*/
        pieceRotator.rotation = Quaternion.Lerp(pieceRotator.localRotation, Quaternion.Euler(objRotation), 10 * Time.deltaTime);

        //pieceRotator.rotation = Quaternion.Lerp(pieceRotator.transform.rotation, Quaternion.Euler(objRotation.x, objRotation.y, objRotation.z), 10 * Time.deltaTime);
        //transform.Rotate(Quaternion.Lerp(transform.rotation, Quaternion.Euler(objRotation.x, objRotation.y, objRotation.z), 10 * Time.deltaTime).eulerAngles, Space.World );
        /*if(transform.rotation.eulerAngles.z >= 259){
            zOrientation =0;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);
        }
        if(transform.rotation.eulerAngles.x >= 259){
            xOrientation =0;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }*/
        
    }
    public void RotatePiece(int x, int y, int z){
        xOrientation += x;
        yOrientation += y;
        zOrientation += z;
        if(xOrientation > 3) xOrientation = 0;
        if(yOrientation > 3) yOrientation = 0;
        if(zOrientation > 3) zOrientation = 0;
        objRotation = new Vector3(xOrientation * 90, yOrientation * 90, zOrientation * 90);
        Debug.LogFormat("rotation  {0}, {1}, {2}", xOrientation, yOrientation, zOrientation);
    }

    private void OnTriggerEnter(Collider other) {
        if(Constants.WALL_TAG.Equals(other.gameObject.tag)){
            var go = other.gameObject;
            if(go.TryGetComponent<WallController>(out WallController wall)){
                wall.TouchWallWithPiece(this);
            }
        }
    }

    internal void KillPlayer(){
        OnComunicateStatusPlayer(0);
    }
}
