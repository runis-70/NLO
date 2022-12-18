using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class BossObject : BaseEnemyObject
{
    private bool isNotMotion = false;
    private bool isDeath = false;
    private bool isEndAnimation = false;
    private bool isMinDistance = false;

    [SerializeField] private protected string nameBoss = "Name";
    [SerializeField] private float maxHealth;
    [SerializeField] private float takenHealth;
    [HideInInspector] public Transform playerTransform;
    private protected float health;
    [SerializeField] private Vector2 maxDistanceToThePlayer;
    private float distanceToThePlayer;

    [Header("Точки ограничения")]
    [SerializeField] private Transform maxPointY;
    [SerializeField] private Transform minPointY;
    [SerializeField] private Transform maxPointX;
    [SerializeField] private Transform minPointX;
    [SerializeField] private Transform pointBossAnimationStart;   
    [Header("Настроки анимации смерти")]
    [SerializeField] private Transform deathPoint;
    [SerializeField] private float decreaseScale;
    private float distanceToDeathPoint;
    private float rotationToDeathPoint;


    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;


    private void FixedUpdate()
    {
        distanceToThePlayer = Vector2.Distance(playerTransform.position, transform.position);
        if (distanceToThePlayer < maxDistanceToThePlayer.x)
            isMinDistance = true;
        else
            isMinDistance = false;

        if (isEndAnimation == true)
        {
            transform.position = new Vector3
         (
            Mathf.Clamp(transform.position.x, minPointX.position.x, maxPointX.position.x),
            Mathf.Clamp(transform.position.y, minPointY.position.y, maxPointY.position.y),
            transform.position.z
         );
        }

        if (isNotMotion == false && isEndAnimation == true)
        {
                Vector2 newPos =
           Vector2.MoveTowards(transform.position,
           new Vector2(playerTransform.position.x + maxDistanceToThePlayer.x, playerTransform.position.y + maxDistanceToThePlayer.y),
           4f * Time.fixedDeltaTime);
            rigidbody2D.MovePosition(newPos);
        }
    }
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerController.Deathed += DeathPlayer;
        health = maxHealth;
        StartCoroutine(AnimationStart(5f));
    }
    private void DeathPlayer()
    {
        isNotMotion = true;
    }
    private void Death()
    {
        isNotMotion = true;
        StartCoroutine(AnimationDeath(2));
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
    private IEnumerator AnimationDeath(float speed)
    {
        distanceToDeathPoint = Vector2.Distance(transform.position, deathPoint.position);
        rotationToDeathPoint = Vector2.SignedAngle(deathPoint.position, transform.forward);
        for (float i = 0; i < 1; i += Time.deltaTime / distanceToDeathPoint * speed)
        {
            distanceToDeathPoint = Vector2.Distance(transform.position, deathPoint.position);
            rotationToDeathPoint = Vector2.SignedAngle(deathPoint.position, transform.position);

            rigidbody2D.MovePosition(Vector2.MoveTowards(transform.position,
          new Vector2(deathPoint.position.x, deathPoint.position.y), 4f * Time.fixedDeltaTime));

          transform.localScale = Vector2.MoveTowards
          (transform.localScale, new Vector2(decreaseScale, decreaseScale), i);

            transform.rotation = Quaternion.RotateTowards
            (transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationToDeathPoint), i) ;
            yield return null;
        }
        for (float i = 0; i < 1; i += Time.deltaTime / distanceToDeathPoint * speed)
        {
            spriteRenderer.color =
        Color.Lerp(spriteRenderer.color, new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0), i / 100);
            yield return null;
        }

        transform.position = deathPoint.position;
        transform.localScale = new Vector2(decreaseScale, decreaseScale);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationToDeathPoint);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);

        isDeath = true;
        Destroy(gameObject);
    }
    private IEnumerator AnimationStart(float speed)
    {
        for (float i = 0; i < 1; i += Time.deltaTime / speed)
        {
            rigidbody2D.MovePosition(Vector2.MoveTowards(transform.position,
          pointBossAnimationStart.position, 4f * Time.fixedDeltaTime));
            print(2356);
            yield return null;
        }
        transform.position = pointBossAnimationStart.position;
        isEndAnimation = true;
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
    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }
}