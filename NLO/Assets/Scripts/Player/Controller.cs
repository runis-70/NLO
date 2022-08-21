using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GenerateController generate;
    [SerializeField] private CanvasUI canvas;
    [SerializeField] private int maxHP;
    private Animator animator;
    private int HP;
    private int scoreNew;

    [Header("Настройка лучей")]
    public GameObject BlueRay;
    public GameObject RedRay;
    public GameObject GreenRay;
    [SerializeField] private float secondRay;
    [SerializeField] private float offsetRay1;
    [SerializeField] private float offsetRay2;

    // События
    public static Action<int> Score;
    public static Action MaxHp;

    private RaycastHit2D hit;
    private void Start()
    {
        HP = maxHP;
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(0, 2), new Vector2(1, 9), 0, new Vector2(0, -1));
            if (hit.collider != null)
            {
                print(hit.collider.tag);
                //Обработка лучом обьектов         
                if (hit.collider.tag == "Mine")
                {
                    Destroy(hit.collider.gameObject);
                    AddScore(-600);
                    RecountHp(-1);
                    StartCoroutine(OnRedRayDown());
                }
                if (hit.collider.tag == "Barn")
                {
                    Destroy(hit.collider.gameObject);
                    AddScore(800);
                    StartCoroutine(OnBlueRayDown());
                }
                if (hit.collider.tag == "Cow")
                {
                    Destroy(hit.collider.gameObject);
                    AddScore(700);
                    StartCoroutine(OnBlueRayDown());
                }
                if (hit.collider.tag == "Tractor")
                {
                    Destroy(hit.collider.gameObject);
                    AddScore(900);
                    StartCoroutine(OnBlueRayDown());
                }
                if (hit.collider.tag == "Health")
                {
                    if(HP == maxHP)
                        AddScore(1000);
                    else
                    {                       
                        RecountHp(1);
                        StartCoroutine(OnGreenRayDown());
                    }
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                StartCoroutine(OnBlueRayDown()); ;
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position, new Vector3(1,9.0f,0));
    //}
    public void RecountHp(int deltahp)//Здоровье
    {
        if (HP > 0)
        {
            HP += deltahp;
        }
        if (HP > maxHP)
        {
            HP = maxHP;
        }
        else if (HP != maxHP)
        {
            MaxHp.Invoke();
        }
        if (HP <= 0)
        {
            animator.SetTrigger("Death");
        }

    }
    public void Death()
    {
        canvas.Lose();
        BlueRay.SetActive(false);
        RedRay.SetActive(false);
        generate.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private IEnumerator OnRedRayDown()
    {
        RedRay.SetActive(true);
        yield return new WaitForSeconds(secondRay);
        RedRay.SetActive(false);
    }
    private IEnumerator OnBlueRayDown()
    {
        BlueRay.SetActive(true);
        yield return new WaitForSeconds(secondRay);
        BlueRay.SetActive(false);
    }
    private IEnumerator OnGreenRayDown()
    {
        GreenRay.SetActive(true);
        yield return new WaitForSeconds(secondRay);
        GreenRay.SetActive(false);
    }
    public int GetHP()
    {
        return HP;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
    private void AddScore(int score)
    {
        scoreNew += score;
        if (scoreNew < 0)
            scoreNew = 0;
        else
            Score.Invoke(scoreNew);
    }
}
