using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InstantiateMobsManager instantiateMobsManager;
    [SerializeField] private MusicManager soundManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int maxCountMurders;
    [SerializeField] private int maxHP;
    private Animator animator;
    private int countMurders = 0;
    private int HP;
    private int score;

    [Header("Настройка лучей")]
    public GameObject BlueRay;
    public GameObject RedRay;
    public GameObject GreenRay;
    [SerializeField] private float secondRay;
    [HideInInspector][SerializeField] private List<string> tagObjects;
    [HideInInspector][SerializeField] private List<int> scoreObjects;

    // События игрока
    public static Action<int> Score;
    public static Action MaxHp;
    public static Action Deathed;
    public static Action <int> CountMurders; 

    private void Start()
    {
        instantiateMobsManager.GetTagObjects(ref tagObjects);
        instantiateMobsManager.GetScoreObjects(ref scoreObjects);
        HP = maxHP;
        animator = GetComponent<Animator>();
        CountMurders.Invoke(countMurders); // Передача данных при старте
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(0, 2), new Vector2(1, 9), 0, new Vector2(0, -1));
            if (hit.collider != null)
            {
                print(hit.collider.tag);
                for (int i = 0; i < tagObjects.Count; i++)
                {
                    if (hit.collider.tag == tagObjects[i])
                    {
                        countMurders++;
                        CountMurders?.Invoke(1);
                        instantiateMobsManager.countEnemy--;
                        if (tagObjects[i] == "Mine")
                        {
                            AddScore(scoreObjects[i]);
                            RecountHp(-1);
                            StartCoroutine(OnRayDown(RedRay, 1));
                            StartCoroutine(DestroyObject(0.1f, hit.collider));
                        }
                        else if (tagObjects[i] == "Health")
                        {
                            RecountHp(1);
                            StartCoroutine(OnRayDown(GreenRay, 0));
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        StartCoroutine(DestroyObject(0.1f, collision.collider));
    }
    private IEnumerator DestroyObject(float second, Collider2D collider)
    {
        yield return new WaitForSeconds(second);
        Destroy(collider.gameObject);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(transform.position, new Vector3(1,9.0f,0));
    //}
    public void RecountHp(int deltahp)// Рассчет здоровье
    {
        HP += deltahp;
        Mathf.Clamp(HP, 0, maxHP);
        if (HP != maxHP)
            MaxHp?.Invoke();
        if (HP == 0)
        {
            SetTrigerDeath();
            DisableСomponents();
            Deathed?.Invoke();
        }
    }
    private void SetTrigerDeath() 
    {
        animator.SetTrigger("Death");
    }
    private void DisableСomponents()
    {
        Destroy(BlueRay.gameObject);
        Destroy(RedRay.gameObject);
        Destroy(GreenRay.gameObject);
        instantiateMobsManager.StopAllCoroutines();
    }
    public void SoundOfDeath()
    {
        soundManager.OnPlayOneShot(2);
    }
    public void AnimationDeathEnd()
    {
        gameManager.Lose();
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
        return countMurders;
    }
    public int GetMaxCountMurders()
    {
        return maxCountMurders;
    }
    private void AddScore(int score)
    {
        this.score += score;
        if (this.score < 0)
            this.score = 0;
        else
            Score?.Invoke(this.score);
    }
}
