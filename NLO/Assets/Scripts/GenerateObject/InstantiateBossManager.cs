using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InstantiateBossManager : GenerateController
{
    [SerializeField] private List<BossObject> bossObjects;
    [SerializeField] private InstantiateMobsManager instantiateMobsManager;
    [SerializeField] private GameManager gameManager;
    private string nameObject;

    private void Awake()
    {
        // Иницилизация
        for (int i = 0; i < bossObjects.Count; i++)
        {
            scoreObjects.Add(bossObjects[i].GetScore());
            tagObjects.Add(bossObjects[i].gameObject.tag);
        }
    }
    private void OnDisable()
    {
        InstantiateMobsManager.isNulledEnemy -= StartInstantiateBossObjectIE;
    }
    private void Start()
    {
       InstantiateMobsManager.isNulledEnemy += StartInstantiateBossObjectIE;
    }
    private IEnumerator InstantiateRandomBossObjectIE()
    {
        yield return new WaitForSeconds(3f);
        gameManager.BossNameCall();
        int random = Random.Range(0, bossObjects.Count);
        if (lastRandomNumber != random & penultimateRandomNumber != random)
        {
            BossObject bossObject = bossObjects[random];
            nameObject = bossObject.GetName();
            InstantiateObject(bossObject);
        }
        penultimateRandomNumber = lastRandomNumber;
        lastRandomNumber = random;
    }
    private void StartInstantiateBossObjectIE()
    {
        StartCoroutine(InstantiateRandomBossObjectIE());
    }
    private void InstantiateObject(BossObject bossObject)
    {
        Instantiate(bossObject, transform.position, bossObject.transform.rotation);
    }
    public string GetNameBoss()
    {
        return nameObject;
    }
}
