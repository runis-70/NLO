using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject BlueRay;
    public GameObject RedRay;
    [SerializeField] private GenerateObject generate;
    [SerializeField] private float secondRay;
    [SerializeField] private int maxHP;
    public static Action YouLose;
    public static Action<float> Score;
    private int HP;
    private void Start()
    {
        HP = maxHP;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            if (hit.collider != null)
            {
                print(hit.collider.tag);
                if (hit.collider.tag == "Enemy")
                {
                    Destroy(hit.collider.gameObject);
                    StartCoroutine(OnRedRayDown());
                    generate.countEnemy--;
                    RecountHp(-1);
                }
                if (hit.collider.tag == "Mine")
                {
                    generate.countMine--;
                    StartCoroutine(OnBlueRayDown());
                    Score.Invoke(300);
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.tag == "Cow")
                {
                    generate.countCow--;
                    StartCoroutine(OnBlueRayDown());
                    Score.Invoke(100);
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.tag == "Tractor")
                {
                    generate.countTractor--; ;
                    StartCoroutine(OnBlueRayDown());
                    Score.Invoke(200);
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {            
                StartCoroutine(OnBlueRayDown()); ;
            }
        }
    }
    public void RecountHp(int deltahp)//Çהמנמגüו
    {
        if (deltahp < 0)
        {
            HP = HP + deltahp;
        }
        else if (HP > maxHP)
        {
            HP = maxHP + deltahp;
            HP = maxHP;
        }
        if (HP <= 0)
        {
            YouLose.Invoke();
        }
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
    public int GetHP()
    {
        return HP;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
}
