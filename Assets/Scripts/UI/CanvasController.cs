using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CanvasController : MonoBehaviour
{

    public enum GameState{
        INGAME, PAUSED, MAINMENU, RETRY, GAMEOVER
    }

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        PieceController.OnComunicateStatusPlayer += UpdatePlayerLife;
    }

    private void OnDestroy() {
        PieceController.OnComunicateStatusPlayer -= UpdatePlayerLife;
    }

    private void FixedUpdate() {
        
    }

    public void ChangeGameState(GameState newState){
        GameManager.Instance.OnChangeState(newState);
    }

    private void UpdatePlayerLife(int life){
        if(life <= 0)
            animator.SetTrigger("gameOver");
    }

    public void SetTimeScale(float newTimeScale){
        Time.timeScale = newTimeScale;
    }
}
