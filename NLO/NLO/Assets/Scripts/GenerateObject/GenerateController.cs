using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateController : MonoBehaviour
{
    [Header("Рандом")]
    [SerializeField] private List<MoveObjects> moveObjects;
    [Header("Задержка рандома")]
    [Range(0f, 20f)]
    [SerializeField] private float minFrame;
    [Range(0f, 20f)]
    [SerializeField] private float maxFrame;
    [HideInInspector] [SerializeField] private List<float> speedObjects;
    [SerializeField] private List<int> countObjects;
    [SerializeField] private List<int> numberObjects;
    [SerializeField] private DeathZone deathZone;

    [SerializeField] private int MaxCountBarn;
    [SerializeField] private int MaxCountMine;
    [SerializeField] private int MaxCountTractor;
    [SerializeField] private int MaxCountCow;
    [SerializeField] private int MaxCountHealth;
    public int countBarn = 0;
    public int countMine = 0;
    public int countTractor = 0;
    public int countCow = 0;
    public int countHealth = 0;

    private void Start()
    {       
        // Иницилизация
        for (int i = 0; i < moveObjects.Count; i++)
        {
            speedObjects.Add(moveObjects[i].GetSpeed());
            countObjects.Add(0);
        }
        // генерация случайного числа
        int random = Random.Range(0, moveObjects.Count);
        // Настройка обьекта
        MoveObjects moveObject = moveObjects[random];
        float speedObject = speedObjects[random];
        moveObject.SetSpeed(speedObject);
        countObjects[random]++;
        numberObjects.Add(random);
        // Запуск функций
        CheckRandomGameObject(moveObject);
        StartCoroutine(enumerator());
    }
    private void FixedUpdate()
    {
        // Добавление скорости обьектам
        for (int i = 0; i < moveObjects.Count; i++)
        {
           if (moveObjects[i].GetSpeed() < moveObjects[i].GetMaxSpeed())
                speedObjects[i] += moveObjects[i].GetAccelaration() * Time.fixedDeltaTime;
        }
    }
    public void GetRange(ref List<int> newCountObjects, ref List<int> newNumberObjects)
    {
        // Получаем количество обьектов
        newCountObjects = countObjects;
        // Получаем номер последнего добавленого обьекта
        newNumberObjects = numberObjects;
    }
    public IEnumerator enumerator()
    {
        // генерация случайного числа
        yield return new WaitForSeconds(Random.Range(minFrame, maxFrame));
        int random = Random.Range(0, moveObjects.Count);
        // Настройка обьекта
        float speedObject = speedObjects[random];
        MoveObjects moveObject = moveObjects[random];
        moveObject.SetSpeed(speedObject);
        countObjects[random]++;
        numberObjects.Add(random);
        // Запуск функций
        CheckRandomGameObject(moveObject);
        StartCoroutine(enumerator());
    }
    public void CheckRandomGameObject(MoveObjects moveObject)
    {
        if (moveObject.tag == "Barn")
        {
            if (countBarn < MaxCountBarn)
            {   
                Instantiate(moveObject, transform.position, Quaternion.identity);
            }
        }
        else if (moveObject.tag == "Tractor")
        {
            if (countTractor < MaxCountTractor)
            {
                Instantiate(moveObject, transform.position, Quaternion.identity);
            }
        }
        else if (moveObject.tag == "Mine")
        {
            if (countMine < MaxCountMine)
            {
                Instantiate(moveObject, transform.position, Quaternion.identity);
            }
        }
        else if (moveObject.tag == "Cow")
        {
            if (countCow < MaxCountCow)
            {
                Instantiate(moveObject, transform.position, Quaternion.identity);
            }
        }
        else if (moveObject.tag == "Health")
        {
            if (countHealth < MaxCountHealth)
            {
                Instantiate(moveObject, transform.position, Quaternion.identity);
            }
        }
        deathZone.GetRange(ref countObjects);
        // Очищаем массив от всех обьектов
        numberObjects.Clear();
    }
}