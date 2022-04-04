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
    public void OnEnable()
    {
        Controller.YouLose -= Death;
    }
    private void Start()
    {
        Controller.YouLose += Death; 
        controller = GetComponent<Controller>();
        animator = GetComponent<Animator>();
    }
    public void End()
    {
        canvas.Lose();
    }
    public void ToEndWay()
    {
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
    public void Death() 
    {
        controller.BlueRay.SetActive(false);
        controller.RedRay.SetActive(false);
        generate.enabled = false;
        controller.enabled = false;
        animator.SetTrigger("Death");

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
