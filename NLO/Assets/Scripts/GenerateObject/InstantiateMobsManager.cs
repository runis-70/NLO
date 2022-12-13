
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class InstantiateMobsManager : GenerateController
{
    [Header("List обьектов")]
    [SerializeField] private List<EnemyObject> moveObjects;

    [Header("Бонусы")]
    [SerializeField] private EnemyObject healthObject;
    private float speedHealthObject;

    [Header("Задержка рандома")]
    [Range(0f, 20f)]
    [SerializeField] private float minFrame;
    [Range(0f, 20f)]
    [SerializeField] private float maxFrame;

   public int countEnemy;
    [SerializeField] private int maxCountEnemy;
    [HideInInspector] [SerializeField] private List<float> speedObjects;
    [HideInInspector] [SerializeField] private List<float> accelerationObjects;
    [HideInInspector] [SerializeField] private List<float> maxSpeedObjects;

    public static Action isNulledEnemy;

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
        PlayerController.MaxHp -= StartInstantiateHealthObjectIE;
    }
    private void Start()
    {
        PlayerController.MaxHp += StartInstantiateHealthObjectIE;
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
        if (countEnemy == 0)
            isNulledEnemy?.Invoke();
    }
    private IEnumerator InstantiateRandomObjectIE()
    {

        yield return new WaitForSeconds(Random.Range(minFrame, maxFrame));
        int random = Random.Range(0, moveObjects.Count);
        if (countEnemy < maxCountEnemy && lastRandomNumber != random && penultimateRandomNumber != random && penultimatePenultimateRandomNumber != random)
        {
            countEnemy++;
            float speedObject = speedObjects[random];
            EnemyObject moveObject = moveObjects[random];
            InstantiateObject(moveObject, speedObject);
            penultimatePenultimateRandomNumber = penultimateRandomNumber;
            penultimateRandomNumber = lastRandomNumber;
            lastRandomNumber = random;
        }
        StartCoroutine(InstantiateRandomObjectIE());
    }
    private void StartInstantiateHealthObjectIE()
    {
        StartCoroutine(InstantiateRandomObjectIE());
    }
    public void StopInstantiateRandomObjectIE()
    {
        StopCoroutine(InstantiateRandomObjectIE());
    }
    public void StartInstantiateRandomObjectIE()
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
    }
    private void InstantiateObject(EnemyObject enemyObject, float speedObject)
    {
       Instantiate(enemyObject, transform.position, enemyObject.transform.rotation).SetSpeed(speedObject);       
    }
}