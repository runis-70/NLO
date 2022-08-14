using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private CanvasUI canvas;
    private Controller controller;
    [SerializeField] private GenerateController generate;
    private void Start()
    {
        controller = GetComponent<Controller>();
        animator = GetComponent<Animator>();
    }
    public void Death() 
    {
        canvas.Lose();
        controller.BlueRay.SetActive(false);
        controller.RedRay.SetActive(false);
        generate.gameObject.SetActive(false);
        controller.gameObject.SetActive(false);
    }
}
