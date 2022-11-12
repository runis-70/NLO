using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : BaseEnemyObject
{
    private Animator animator;
    [SerializeField] private protected string nameBoss = "Name";

    private void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController.Deathed += DeathPlayer;
    }
    private void DeathPlayer()
    {
        animator.SetBool("BossIsBack", true);
    }
    private void OnDisable()
    {
        PlayerController.Deathed += DeathPlayer;
    }
    public string GetName()
    {
        return nameBoss;
    }
}