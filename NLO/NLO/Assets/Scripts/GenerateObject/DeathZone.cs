using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GenerateController generateController;
    [SerializeField] private List<int> countObjects;
     [SerializeField] private List<int> numberObjects;

    public void GetRange(ref List<int> newCountObjects)
    {
        newCountObjects = countObjects;
    }
    private void FixedUpdate()
    {
        // Получения данных
        generateController.GetRange(ref countObjects, ref numberObjects);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barn")
        {
            if (countObjects[numberObjects[0]] != 0)
            {
                countObjects[numberObjects[0]]--;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Mine")
        {
            if (countObjects[numberObjects[0]] != 0)
            {
                countObjects[numberObjects[0]]--;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Tractor")
        {
            if (countObjects[numberObjects[0]] != 0)
            {
                countObjects[numberObjects[0]]--;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Cow")
        {
            if (countObjects[numberObjects[0]] != 0)
            {
                countObjects[numberObjects[0]]--;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Health")
        {
            if (countObjects[numberObjects[0]] != 0)
            {
                countObjects[numberObjects[0]]--;
            }
            Destroy(collision.gameObject);
        }
        // удаления элемента из массива
        numberObjects.Remove(0);
    }
}
