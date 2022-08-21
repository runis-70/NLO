using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barn")
        {
             Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Mine")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Tractor")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Cow")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Health")
        {
            Destroy(collision.gameObject);
        }
    }
}
