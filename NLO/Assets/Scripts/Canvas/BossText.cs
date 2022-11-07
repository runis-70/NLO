using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossText : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string bossCallText;
    [SerializeField] private Text bossText;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SelectTextBossCall()
    {
        bossText.text = bossCallText;
    }
    public void SelectTextNameBoss(string nameBossText)
    {
        animator.SetBool("Anim", true);
        bossText.text = nameBossText;
    }
}
