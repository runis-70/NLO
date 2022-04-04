using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObject : MonoBehaviour
{
    public GameObject[] objects;
    [SerializeField] private int MaxCountEnemy;
    [SerializeField] private int MaxCountMine;
    [SerializeField] private int MaxCountTractor;
    public int countEnemy;
    public int countMine;
    public int countTractor;
    public void OnEnable()
    {
        Instantiate(objects[Random.Range(0, objects.Length - 1)], transform.position, Quaternion.identity);
        StartCoroutine(enumerator());
    }
    public IEnumerator enumerator()
    {
        yield return new WaitForSeconds(Random.Range(4,5));
        int random = Random.Range(0, objects.Length);
        GameObject gameObject = objects[random];
        if (gameObject.tag == "Enemy")
        {
            if (countEnemy < MaxCountEnemy )
            {
                Instantiate(objects[random], transform.position, Quaternion.identity);
                countEnemy++;
            }
        }
        else if (gameObject.tag == "Tractor")
        {
            if (countTractor < MaxCountTractor)
            {
                Instantiate(objects[random], transform.position, Quaternion.identity);
                countTractor++;
            }
        }
        else if(gameObject.tag == "Mine")
        {
            if (countMine < MaxCountMine)
            {
                Instantiate(objects[random], transform.position, Quaternion.identity);
                countMine++;
            }
        }
        StartCoroutine(enumerator());
    }
}
