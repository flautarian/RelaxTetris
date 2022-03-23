using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallConfig", menuName = "Walls/WallConfig", order = 0)]
public class WallConfig : ScriptableObject
{
    [SerializeField] private int[] wallPixelDistribution;
    [SerializeField] private Vector3 successfulRotation;
    [SerializeField] private string pieceTag;

    public Vector3 SuccessfulRotation => successfulRotation;
    public int[] WallPixelDistribution => wallPixelDistribution;
    public string PieceTag => pieceTag;
}


