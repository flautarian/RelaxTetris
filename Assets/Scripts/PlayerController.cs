using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float velocity =0;

    [SerializeField]
    private Rigidbody pieceRb;
    private Ray ray;
    private Vector3 newPoint = Vector3.zero;

    [SerializeField]
    private GameObject currentPiece;

    private void Start() {
        StartCoroutine(LoadPieceFromManager());
    }

    private IEnumerator LoadPieceFromManager(){
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameObject newPiece = GameManager.Instance.LoadPlayerPiece();
        newPiece.SetActive(true);
        newPiece.transform.parent = currentPiece.transform.parent;
        newPiece.transform.localPosition = Vector3.zero;
        Destroy(currentPiece.gameObject);
        currentPiece = newPiece;
        if(newPiece.TryGetComponent<PieceController>(out PieceController piece)){
            piece.playerController = this;
        }
    }

    internal void Die(){

    }
}
