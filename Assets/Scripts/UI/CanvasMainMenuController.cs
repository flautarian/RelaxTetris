using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CanvasMainMenuController : MonoBehaviour
{

    public static event Action<CanvasController.GameState> OnChangeState = delegate { };
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    public void GoToGame(){
        SceneManager.LoadScene(1);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
