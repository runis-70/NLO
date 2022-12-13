using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossObject : BaseEnemyObject
{
    private Animator animator;
    private bool isNotMotion = false;
    private bool isDeath = false;
    [SerializeField] private protected string nameBoss = "Name";
    [SerializeField] private float maxHealth;
    [SerializeField] private float takenHealth;
    private protected float health;
    [SerializeField] private Vector2 distanceToThePlayer;


    private Rigidbody2D rigidbody2D;

    [HideInInspector] public Transform playerTransform;


    private void FixedUpdate()
    {
        if (isDeath)
        {

        }
        if (isNotMotion == false)
        {
            Vector2 newPos =
           Vector2.MoveTowards(transform.position,
           new Vector2(playerTransform.position.x + distanceToThePlayer.x, playerTransform.position.y + distanceToThePlayer.y),
           4f * Time.fixedDeltaTime);
            rigidbody2D.MovePosition(newPos);
        }
    }

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerController.Deathed += DeathPlayer;
        health = maxHealth;
        StartCoroutine(WaitPlayer(3));
    }
    private void DeathPlayer()
    {
        isNotMotion = true;
        animator.SetBool("BossIsBack", true);
    }
    private void Death()
    {
        isDeath = true;
        isNotMotion = true;
        animator.SetBool("Death", true);
    }
    public void RecountHp(float deltahp)
    {
        health += deltahp;
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health == 0)
            Death(); 
    }
    private void OnDisable()
    {
        PlayerController.Deathed -= DeathPlayer;
    }
    public string GetName()
    {
        return nameBoss;
    }
    private IEnumerator TakeHealth(float second)
    {
        RecountHp(-takenHealth);
        yield return new WaitForSeconds(second);
        StartCoroutine(TakeHealth(second));
    }
    private IEnumerator WaitPlayer(float second)
    {
        yield return new WaitForSeconds(second);
        StartCoroutine(TakeHealth(0.5f));
    }
    public void OnDestroy()
    {
        StopAllCoroutines();
    }
    public float GetHealth()
    {
        print(158);
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public bool GetIsDeath()
    {
        return isDeath;
    }
}