using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationControllerMenu : MonoBehaviour
{
    [SerializeField] private string nameScene;
    private Animator animator;

    private void Start()
    {
        CanvasMenu.WayStarted += WayStart;
        animator = GetComponent<Animator>();
    }
    private void OnDisable()
    {
        CanvasMenu.WayStarted -= WayStart;
    }
    public void StartScene()
    {
       SceneManager.LoadScene(nameScene);
    }
    private void WayStart()
    {
        animator.SetInteger("Anim", 1);
    }
    private void OnMouseDown()
    {
        animator.SetInteger("Anim", 1);
    }
}
