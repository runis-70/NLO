using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    public GameObject[] objects;
    [Header("Настройка скорости")]
    [Header("Враг")]
    [SerializeField] private float accelerationEnemy;
    [SerializeField] private float MaxSpeedEnemy;
    [SerializeField] private float speedEnemy;
    [Header("Дом")]
    [SerializeField] private float accelerationMine;
    [SerializeField] private float MaxSpeedMine;
    [SerializeField] private float speedMine;
    [Header("Корова")]
    [SerializeField] private float accelerationCow;
    [SerializeField] private float MaxSpeedCow;
    [SerializeField] private float speedCow;
    [Header("Трактор")]
    [SerializeField] private float accelerationTractor;
    [SerializeField] private float MaxSpeedTractor;
    [SerializeField] private float speedTractor;
    [Header("Рандом")]
    [SerializeField] private int MaxCountEnemy;
    [SerializeField] private int MaxCountMine;
    [SerializeField] private int MaxCountTractor;
    [SerializeField] private int MaxCountCow;
    public int countEnemy;
    public int countMine;
    public int countTractor;
    public int countCow;
    private void FixedUpdate()
    {
        if (speedEnemy < MaxSpeedEnemy)
        {
            speedEnemy += accelerationEnemy * Time.fixedDeltaTime;
        }
        if (speedMine < MaxSpeedMine)
        {
            speedMine += accelerationMine * Time.fixedDeltaTime;
        }
        if (speedCow < MaxSpeedCow)
        {
            speedCow += accelerationCow * Time.fixedDeltaTime;
        }
        if (speedTractor < MaxSpeedTractor)
        {
            speedTractor += accelerationTractor * Time.fixedDeltaTime;
        }
    }
    public void OnEnable()
    {
        Instantiate(objects[Random.Range(0, objects.Length - 1)], transform.position, Quaternion.identity);
        StartCoroutine(enumerator());
    }
    public IEnumerator enumerator()
    {
        yield return new WaitForSeconds(Random.Range(3f,4f));
        int random = Random.Range(0, objects.Length);
        GameObject gameObject = objects[random];
        if (gameObject.tag == "Enemy")
        {
            if (countEnemy < MaxCountEnemy )
            {
                gameObject.GetComponent<MoveObjects>().speed = speedEnemy;
                Instantiate(gameObject, transform.position, Quaternion.identity);
                countEnemy++;
            }
        }
        else if (gameObject.tag == "Tractor")
        {
            if (countTractor < MaxCountTractor)
            {
                gameObject.GetComponent<MoveObjects>().speed = speedTractor;
                Instantiate(gameObject, transform.position, Quaternion.identity);
                countTractor++;
            }
        }
        else if(gameObject.tag == "Mine")
        {
            if (countMine < MaxCountMine)
            {
                gameObject.GetComponent<MoveObjects>().speed = speedMine;
                Instantiate(gameObject, transform.position, Quaternion.identity);
                countMine++;
            }
        }
        else if (gameObject.tag == "Cow")
        {
            if (countCow < MaxCountCow)
            {
                gameObject.GetComponent<MoveObjects>().speed = speedCow;
                Instantiate(gameObject, transform.position, Quaternion.identity);
                countCow++;
            }
        }
        StartCoroutine(enumerator());
    }
}
