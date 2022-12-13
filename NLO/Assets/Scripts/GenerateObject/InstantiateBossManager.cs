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
    [SerializeField] private Transform playerTransform;
    public BossObject currentBoss;
    private bool bossExists = false;
    private string nameObject;

    public event Action <bool> BossDeathed;
    public event Action<bool> BossCreated;


    private void Awake()
    {
        // Иницилизация
        for (int i = 0; i < bossObjects.Count; i++)
        {
            scoreObjects.Add(bossObjects[i].GetScore());
            tagObjects.Add(bossObjects[i].gameObject.tag);
        }
    }
    private void Update()
    {
        if (currentBoss != null && currentBoss.GetIsDeath() == true)
        {
            BossDeath();
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
        if (gameManager.GetCountMurders() == gameManager.GetMaxCountMurders() && bossExists == false)
        {
            yield return new WaitForSeconds(3f);
            gameManager.GetNameBoss();
            int random = Random.Range(0, bossObjects.Count);
            if (lastRandomNumber != random & penultimateRandomNumber != random)
            {
                BossObject bossObject = bossObjects[random];
                bossObject.playerTransform = playerTransform;
                nameObject = bossObject.GetName();
                InstantiateObject(bossObject);
                BossCreated?.Invoke(bossExists);
                currentBoss = FindObjectOfType<BossObject>();
                gameManager.MaxValueBossHealthSlider(currentBoss.GetMaxHealth());
            }
            penultimateRandomNumber = lastRandomNumber;
            lastRandomNumber = random;
        }
    }
    private void StartInstantiateBossObjectIE()
    {
        StartCoroutine(InstantiateRandomBossObjectIE());
    }
    private void InstantiateObject(BossObject bossObject)
    {
        Instantiate(bossObject, transform.position, bossObject.transform.rotation);
        bossExists = true;
    }
    private void BossDeath()
    {
        BossDeathed?.Invoke(false);
        bossExists = false;
    }
    public string GetNameBoss()
    {
        return nameObject;
    }
    public float GetBossHealth()
    {
        if (currentBoss != null)
        {
            return currentBoss.GetHealth();
        }
        else
            return 100;
    }
    public float GetBossMaxHealth()
    {
        if (currentBoss != null)
        {
            return currentBoss.GetMaxHealth();
        }
        else
            return 100;
    }
}
