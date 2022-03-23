using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Color evenColor = Color.white;
    public Color oddColor = Color.white;

    public float evenScale;
    public float oddScale;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
