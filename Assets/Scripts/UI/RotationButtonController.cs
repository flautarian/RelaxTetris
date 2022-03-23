using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RotationButtonController : MonoBehaviour
{
    [SerializeField]
    private int rotationPointX;

    [SerializeField]
    private int rotationPointY;

    [SerializeField]
    private int rotationPointZ;

    private Animator animator;

    public static event Action<int, int, int> OnChangeRotation = delegate { };
    
    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0) && GameManager.Instance.IsInGame()){
            OnChangeRotation(rotationPointX, rotationPointY, rotationPointZ);
            animator.SetTrigger("selected");
        }
    }
}
