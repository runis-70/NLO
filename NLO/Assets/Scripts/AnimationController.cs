using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private CanvasUI canvas;
    [SerializeField] private GameManager gameManager;
    private Controller controller;
    private bool isStart = true;
    [SerializeField] private GenerateObject generate;
    private void Start()
    {
        controller = GetComponent<Controller>();
        animator = GetComponent<Animator>();
    }
    public void ToEndWay()
    {
        animator.SetInteger("Anim", 1);
        controller.enabled = true;
        generate.enabled = true;
    }
    public void OnVisibleUI()
    {
        canvas.OnVisible();
    }
    public void StartScrollingBackground()
    {
        gameManager.enabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            canvas.Lose();
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) & isStart)
        {
            animator.SetInteger("Anim", 2);
            isStart = false;
        }
    }
}
