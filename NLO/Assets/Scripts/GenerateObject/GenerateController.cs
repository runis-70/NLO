using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateController : MonoBehaviour
{
    [Header("Рандом")]
    [SerializeField] private List<MoveObjects> moveObjects;
    [Header("Бонусы")]
    [SerializeField] private MoveObjects healthObject;
    private float speedHealthObject;
    [Header("Задержка рандома")]
    [Range(0f, 20f)]
    [SerializeField] private float minFrame;
    [Range(0f, 20f)]
    [SerializeField] private float maxFrame;
     [SerializeField] private List<float> speedObjects;
    [HideInInspector] [SerializeField] private List<float> accelerationObjects;
    [HideInInspector] [SerializeField] private List<float> maxSpeedObjects;
    private int lastRandomNumber = -1;


    private void Awake()
    {
        // Иницилизация
        for (int i = 0; i < moveObjects.Count; i++)
        {
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
        int random = Random.Range(0, moveObjects.Count);
        // Настройка обьекта
        MoveObjects moveObject = moveObjects[random];
        float speedObject = speedObjects[random];
        lastRandomNumber = random;
        // Запуск функций
        InstantiateObject(moveObject, speedObject);
        // Запуск корутин
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
        yield return new WaitForSeconds(Random.Range(minFrame, maxFrame));
        int random = Random.Range(0, moveObjects.Count);
        if (lastRandomNumber != random)
        {
            float speedObject = speedObjects[random];
            MoveObjects moveObject = moveObjects[random];
            InstantiateObject(moveObject, speedObject);          
        }
        lastRandomNumber = random;
        StartCoroutine(InstantiateRandomObjectIE());
    }
    private void StartInstantiateHealthObjectIE()
    {
        StartCoroutine(InstantiateRandomObjectIE());
    }
    private IEnumerator InstantiateHealthObjectIE()
    {
        yield return new WaitForSeconds(Random.Range(minFrame, maxFrame));
        float random = Random.Range(0f, 100f);
        if (random == 0.333f)
        {
            InstantiateObject(healthObject, speedHealthObject);
            StopCoroutine(InstantiateHealthObjectIE());
        }
        StartCoroutine(InstantiateRandomObjectIE());
    }
    private void InstantiateObject(MoveObjects moveObject, float speedObject)
    {
       Instantiate(moveObject, transform.position, Quaternion.identity).SetSpeedFixed(speedObject);       
    }
}