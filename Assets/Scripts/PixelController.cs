using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelController : MonoBehaviour
{
    [SerializeField] WallController wallController;

    [SerializeField] private int pixelId;

    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable() {
        StartCoroutine(CheckSelfActiveInWall());
    }

    private IEnumerator CheckSelfActiveInWall(){
        yield return new WaitUntil(() => wallController != null && wallController.wallConfig != null);
        if(wallController != null && wallController.wallConfig != null){
            gameObject.SetActive(wallController.wallConfig.WallPixelDistribution[pixelId] == 1);
        }   
    }
    // Update is called once per frame
    void Update()
    {
        if(wallController != null){
            transform.rotation = Quaternion.Euler(0, wallController.rotation, 0);
            transform.localScale = new Vector3(wallController.scale, wallController.scale, wallController.scale);
            meshRenderer.material.color = wallController.cubesColor;
        }
    }
}
