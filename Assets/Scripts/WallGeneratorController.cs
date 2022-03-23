using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WallGeneratorController : MonoBehaviour
{
    [SerializeField] private float timeToDeployWall;
    [SerializeField] private float timeInterval;

    [SerializeField] internal WallConfig[] availableWalls;
    void Start()
    {
        timeToDeployWall = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToDeployWall + timeInterval < Time.time){
            GameObject newWall = GameManager.Instance.RequestAndExecuteGameObject("Prefabs/Walls/basic_wall", Vector3.zero);
            if(newWall.TryGetComponent<WallController>(out WallController wall)){
                wall.wallGeneratorController = this;
                wall.wallConfig = availableWalls[UnityEngine.Random.Range(0, availableWalls.Length)];
                wall.initialPosition = GetRandomInitialPosition();
                newWall.transform.parent = this.transform;
                newWall.transform.position = GenerateInitialPosition(wall);
                newWall.SetActive(true);
            }
            timeToDeployWall = Time.time;
        }
    }

    private Vector3 GenerateInitialPosition(WallController wall){
        switch(wall.initialPosition){
            case WallController.INITIAL_POSITION.TOP:
                return new Vector3(0,30,0);
            case WallController.INITIAL_POSITION.BOTTOM:
                return new Vector3(0,-30,0);
            case WallController.INITIAL_POSITION.FRONT:
                return new Vector3(30,0,0);
            default: // WallController.INITIAL_POSITION.BACK:
                return new Vector3(-30,0,0);
        }
    }

    private WallController.INITIAL_POSITION GetRandomInitialPosition(){
        Type type = typeof(WallController.INITIAL_POSITION);
        Array values = type.GetEnumValues();
        int index = UnityEngine.Random.Range(0, values.Length);
        return (WallController.INITIAL_POSITION)values.GetValue(index);
    }
}
