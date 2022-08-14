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
        animator = GetComponent<Animator>();
    }
    public void StartScene()
    {
       SceneManager.LoadScene(nameScene);
    }
    private void OnMouseDown()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetInteger("Anim", 2);
        }
    }
}
