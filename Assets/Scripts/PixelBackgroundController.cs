using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBackgroundController : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField] private bool isOdd = false;
    [SerializeField] private BackgroundController bController;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        meshRenderer.material.color = isOdd? bController.oddColor : bController.evenColor;
    }
}
