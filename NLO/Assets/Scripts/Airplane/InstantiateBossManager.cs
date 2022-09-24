using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBossManager : MonoBehaviour
{
    [Header("List обьектов")]
    [SerializeField] private List<BossObject> bossObjects;

    [HideInInspector][SerializeField] private List<float> speedObjects;
    [HideInInspector][SerializeField] private List<float> accelerationObjects;
    [HideInInspector][SerializeField] private List<float> maxSpeedObjects;
    [HideInInspector][SerializeField] private List<string> tagObjects;
    [HideInInspector][SerializeField] private List<int> scoreObjects;

    private int lastRandomNumber = -1;
    private int penultimateRandomNumber = -1;


    private void Awake()
    {
        // Иницилизация
        for (int i = 0; i < bossObjects.Count; i++)
        {
            scoreObjects.Add(bossObjects[i].GetScore());
            tagObjects.Add(bossObjects[i].gameObject.tag);
            speedObjects.Add(bossObjects[i].GetSpeed());
            accelerationObjects.Add(bossObjects[i].GetAccelaration());
            maxSpeedObjects.Add(bossObjects[i].GetMaxSpeed());
        }
    }
    private void OnDisable()
    {
       GameManager.BossCalled -= StartInstantiateBossObjectIE;
    }
    private void Start()
    {
        GameManager.BossCalled += StartInstantiateBossObjectIE;
    }
    private void FixedUpdate()
    {
        // Добавление скорости обьектам
        for (int i = 0; i < bossObjects.Count; i++)
        {
            if (speedObjects[i] < maxSpeedObjects[i])
                speedObjects[i] += accelerationObjects[i] * Time.fixedDeltaTime;
        }
    }
    private IEnumerator InstantiateRandomBossObjectIE()
    {
            int random = Random.Range(0, bossObjects.Count);
            if (lastRandomNumber != random & penultimateRandomNumber != random)
            {
                float speedObject = speedObjects[random];
                BossObject bossObject = bossObjects[random];
                InstantiateObject(bossObject, speedObject);
            }
            penultimateRandomNumber = lastRandomNumber;
            lastRandomNumber = random;
            yield return null;   
    }
    private void StartInstantiateBossObjectIE()
    {
        StartCoroutine(InstantiateRandomBossObjectIE());
    }
    private void InstantiateObject(BossObject bossObject, float speedObject)
    {
        Instantiate(bossObject, transform.position, bossObject.transform.rotation).SetSpeedFixed(speedObject);
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
