using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GenerateObject objects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (objects.countEnemy > 0)
            {
                objects.countEnemy--;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Mine")
        {
            if (objects.countMine > 0)
            {
                objects.countMine--;
            }
            Destroy(collision.gameObject);
        }
    }
}
