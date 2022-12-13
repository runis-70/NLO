using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossText : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Text bossNameText;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetTextNameBoss(string nameBoss)
    {
        animator.SetBool("Anim", true);
        bossNameText.text = nameBoss;
    }
}
