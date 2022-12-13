using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
    [SerializeField] private string nameScene;
    private Animator animator;
    private Image image;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
        AnimationControllerMenu.SceneStarted += OpenSceneAnim;
    }
    private void OnDisable()
    {
        AnimationControllerMenu.SceneStarted -= OpenSceneAnim;
    }
    private void Start()
    {
        CloseSceneAnim();
    }
    public void OpenSceneAnim()
    {
        image.enabled = true;
        animator.SetTrigger("OpenScene");
    }
    public void CloseSceneAnim()
    {
        image.enabled = true;
        animator.SetTrigger("CloseScene");
    }
    public void Disable()
    {
        image.enabled = false;
    }
    public void StartScene()
    {
        SceneManager.LoadScene(nameScene);
    }
}
