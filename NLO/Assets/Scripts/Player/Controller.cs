using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GenerateController generateController;
    [SerializeField] private MusicManager soundManager;
    [SerializeField] private GameManager canvas;
    [SerializeField] private int maxHP;
    private Animator animator;
    private int numberOfMurders;
    private int HP;
    private int scoreNew;

    [Header("Настройка уничтожения обьектов")]
    public GameObject BlueRay;
    public GameObject RedRay;
    public GameObject GreenRay;
    [SerializeField] private float secondRay;
    [SerializeField] private float offsetRay1;
    [SerializeField] private float offsetRay2;
    [HideInInspector] [SerializeField] private List<string> tagObjects;
    [HideInInspector] [SerializeField] private List<int> scoreObjects;

    // События
    public static Action<int> Score;
    public static Action MaxHp;
    public static Action FadeSceneStarted;

    private RaycastHit2D hit;
    private void Start()
    {
        generateController.GetTagObjects(ref tagObjects);
        generateController.GetScoreObjects(ref scoreObjects);
        FadeSceneStarted?.Invoke();
        HP = maxHP;
        animator = GetComponent<Animator>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        StartCoroutine(DestroyObject(0.1f, collision.collider));
    }
    private IEnumerator DestroyObject(float second, Collider2D collider)
    {
        yield return new WaitForSeconds(second);
        Destroy(collider.gameObject);
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
                for (int i = 0; i < tagObjects.Count; i++)
                {
                    if (hit.collider.tag == tagObjects[i])
                    {
                        numberOfMurders++;
                        generateController.countEnemy--;
                        if (tagObjects[i] == "Mine")
                        {
                            AddScore(scoreObjects[i]);
                            RecountHp(-1);
                            StartCoroutine(OnRayDown(RedRay, 1));
                            StartCoroutine(DestroyObject(0.1f, hit.collider));
                        }
                        else if (tagObjects[i] == "Health")
                        {
                            if (HP == maxHP)
                                AddScore(scoreObjects[i]);
                            else
                            {
                                RecountHp(1);
                                StartCoroutine(OnRayDown(GreenRay, 0));
                            }
                            StartCoroutine(DestroyObject(0.1f, hit.collider));
                        }
                        else
                        {
                            AddScore(scoreObjects[i]);
                            StartCoroutine(OnRayDown(BlueRay, 0));
                            StartCoroutine(DestroyObject(0.1f, hit.collider));
                        }
                    }
                }
            }
            else
            {
                StartCoroutine(OnRayDown(BlueRay, 0));
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
            MaxHp?.Invoke();
        }
        if (HP <= 0)
        {
            animator.SetTrigger("Death");
        }

    }
    public void OnDeath()
    {      
        Destroy(BlueRay.gameObject);
        Destroy(RedRay.gameObject);
        Destroy(GreenRay.gameObject);
        generateController.gameObject.SetActive(false);
    }
    public void Death()
    {
        soundManager.OnPlayOneShot(2);
    }
    public void DeathEnd()
    {
        canvas.Lose();
        gameObject.SetActive(false);
    }
    private IEnumerator OnRayDown(GameObject Ray, int idMusic)
    {
        if (Ray.tag == "Ray")
        {
            Ray.SetActive(true);
            soundManager.OnPlayOneShot(idMusic);
            yield return new WaitForSeconds(secondRay);
            Ray.SetActive(false);
        }
    }
    public int GetHP()
    {
        return HP;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
    public int GetNumberOfMurders()
    {
        return numberOfMurders;
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
