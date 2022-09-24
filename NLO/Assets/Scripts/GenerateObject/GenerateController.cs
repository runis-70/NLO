using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateController : MonoBehaviour
{
    [Header("List обьектов")]
    [SerializeField] private List<MoveObjects> moveObjects;

    [Header("Бонусы")]
    [SerializeField] private MoveObjects healthObject;
    private float speedHealthObject;

    [Header("Задержка рандома")]
    [Range(0f, 20f)]
    [SerializeField] private float minFrame;
    [Range(0f, 20f)]
    [SerializeField] private float maxFrame;

    [HideInInspector] public int countEnemy;
    [SerializeField] private int maxCountEnemy;
    [HideInInspector] [SerializeField] private List<float> speedObjects;
    [HideInInspector] [SerializeField] private List<float> accelerationObjects;
    [HideInInspector] [SerializeField] private List<float> maxSpeedObjects;
    [HideInInspector] [SerializeField] private List<string> tagObjects;
    [HideInInspector] [SerializeField] private List<int> scoreObjects;

    private int lastRandomNumber = -1;
    private int penultimateRandomNumber = -1;


    private void Awake()
    {
        // Иницилизация
        for (int i = 0; i < moveObjects.Count; i++)
        {
            scoreObjects.Add(moveObjects[i].GetScore());
            tagObjects.Add(moveObjects[i].gameObject.tag);
            speedObjects.Add(moveObjects[i].GetSpeed());
            accelerationObjects.Add(moveObjects[i].GetAccelaration());
            maxSpeedObjects.Add(moveObjects[i].GetMaxSpeed());
        }
    }
    private void OnDisable()
    {
        Controller.MaxHp -= StartInstantiateHealthObjectIE;
    }
    private void Start()
    {
        Controller.MaxHp += StartInstantiateHealthObjectIE;
        // генерация случайного числа
        StartCoroutine(InstantiateRandomObjectIE());
    }
    private void FixedUpdate()
    {
        // Добавление скорости обьектам
        for (int i = 0; i < moveObjects.Count; i++)
        {
           if (speedHealthObject < healthObject.GetMaxSpeed())
                speedHealthObject += healthObject.GetAccelaration() * Time.fixedDeltaTime;
           if (speedObjects[i] < maxSpeedObjects[i])
                speedObjects[i] += accelerationObjects[i] * Time.fixedDeltaTime;
        }
    }
    private IEnumerator InstantiateRandomObjectIE()
    {
        if (countEnemy < maxCountEnemy)
        {
            int random = Random.Range(0, moveObjects.Count);
            if (lastRandomNumber != random & penultimateRandomNumber != random)
            {
                countEnemy++;   
                float speedObject = speedObjects[random];
                MoveObjects moveObject = moveObjects[random];
                InstantiateObject(moveObject, speedObject);
            }
            penultimateRandomNumber = lastRandomNumber;
            lastRandomNumber = random;
            yield return new WaitForSeconds(Random.Range(minFrame, maxFrame));
            StartCoroutine(InstantiateRandomObjectIE());
        }
    }
    private void StartInstantiateHealthObjectIE()
    {
        StartCoroutine(InstantiateRandomObjectIE());
    }
    private IEnumerator InstantiateHealthObjectIE()
    {
        yield return new WaitForSeconds(Random.Range(minFrame, maxFrame));
        float random = Random.Range(0, 100);
        if (random == 1)
        {
            InstantiateObject(healthObject, speedHealthObject);
            StopCoroutine(InstantiateHealthObjectIE());
        }
        StartCoroutine(InstantiateRandomObjectIE());
    }
    private void InstantiateObject(MoveObjects moveObject, float speedObject)
    {
       Instantiate(moveObject, transform.position, moveObject.transform.rotation).SetSpeedFixed(speedObject);       
    }
    public void GetTagObjects(ref List<string> newTagObjects) // Передача массива тегов
    {
        newTagObjects = tagObjects;
    }
    public void GetScoreObjects(ref List<int> newScoreObjects) // Передача массива очков обьектов
    {
        newScoreObjects = scoreObjects;
    }
}