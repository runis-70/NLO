using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private CanvasUI canvas;
    private Controller controller;
    private bool isStart = true;
    private void Start()
    {
        controller = GetComponent<Controller>();
        animator = GetComponent<Animator>();
    }
    public void ToEndWay()
    {
        animator.SetInteger("Anim", 1);
        controller.enabled = true;
    }
    public void OnVisibleUI()
    {
        canvas.OnVisible();
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
