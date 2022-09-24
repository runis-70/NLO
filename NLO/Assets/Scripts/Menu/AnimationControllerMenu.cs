using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationControllerMenu : MonoBehaviour
{
    private Animator animator;
    public static Action SceneStarted;

    private void Start()
    {
        CanvasMenu.WayStarted += WayStart;
        animator = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        CanvasMenu.WayStarted -= WayStart;
    }
    public void StartScene()
    {
       SceneStarted?.Invoke();
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
