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

    [Header("Настроки передвижения игрока по оси Y")]
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Rigidbody2D rigibodyParentPlayer;
    [SerializeField] private Transform transformParentPlayer;
    [SerializeField] private float speedY;
    [SerializeField] private float speedX;
    private Vector2 moveInput;
    private bool isMotion = false;
    [Header("Точки ограничения")]
    [SerializeField] private Transform maxPointY;
    [SerializeField] private Transform minPointY;
    [SerializeField] private Transform maxPointX;
    [SerializeField] private Transform minPointX;

    [Header("Настройка лучей")]
    public GameObject BlueRay;
    public GameObject RedRay;
    public GameObject GreenRay;
    [SerializeField] private float secondRay;
    [HideInInspector][SerializeField] private List<string> tagObjects;
    [HideInInspector][SerializeField] private List<int> scoreObjects;

    // Приватные переменные
    private Animator animator;
    private CircleCollider2D circleCollider;
    private int countMurders = 0;
    private int HP;
    private int score;
    private bool isDeath;

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
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        CountMurders.Invoke(countMurders); // Передача данных при старте
    }
    private void FixedUpdate()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        rigibodyParentPlayer.velocity = new Vector2(moveInput.x * speedX, moveInput.y * speedY);


        if (moveInput.x == 0 && moveInput.y == 0 )
            isMotion = false;
        else
            isMotion = true;

        animator.SetBool("isMotion", isMotion);


        transformParentPlayer.position = new Vector3
          (
           Mathf.Clamp(transformParentPlayer.position.x, minPointX.position.x, maxPointX.position.x),
           Mathf.Clamp(transformParentPlayer.position.y, minPointY.position.y, maxPointY.position.y),
           transformParentPlayer.position.z
          );
    }
    public void EnabledAnimator(bool enable)
    {
        animator.enabled = enable;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // StartCoroutine(DestroyObject(collision.collider));
    }
    private IEnumerator DestroyObject(float second, Collider2D collider)
    {
        yield return new WaitForSeconds(second);
        collider.GetComponent<EnemyObject>().Death();
    }
    private IEnumerator DestroyObject(Collider2D collider)
    {
        collider.GetComponent<EnemyObject>().Death();
        yield return null;
    }
    public void RecountHp(int deltahp)// Рассчет здоровье
    {
        HP += deltahp;
        Mathf.Clamp(HP, 0, maxHP);
        if (HP != maxHP)
            MaxHp?.Invoke();
        if (HP == 0)
        {
            SetTrigerDeath();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position + new Vector3(0, -3.1f, 0), new Vector2(1, 4.5f));
    }
    public void KillEnemies()
    {      
        if (isDeath == false)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position + new Vector3(0, -3.1f, 0), new Vector2(1, 4.5f), 0, new Vector2(0, -1));
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
    private void SetTrigerDeath() 
    {
        animator.SetTrigger("Death");
        Deathed?.Invoke();
    }
    private void DisableСomponents() // Отключает компоненты
    {
        isDeath = true;
        instantiateMobsManager.StopAllCoroutines();
        RedRay.SetActive(false);
        GreenRay.SetActive(false);
        BlueRay.SetActive(false);
    }
    private void SoundOfDeath()
    {
        soundManager.OnPlayOneShot(2);
    }
    private void AnimationDeathEnd()
    {
        circleCollider.enabled = false;
        gameManager.Lose();
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
    private void InstantiateEffect(GameObject effect)
    {
        Instantiate(effect, transform.position, transform.rotation);
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
