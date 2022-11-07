using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [HideInInspector][SerializeField] private List<string> tagArray;
    [SerializeField] private InstantiateMobsManager instantiateMobsManager;

    private void Start()
    {
        instantiateMobsManager.GetTagObjects(ref tagArray);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < tagArray.Count; i++)
        {
            if (collision.gameObject.tag == tagArray[i])
            {
                instantiateMobsManager.countEnemy--;
                Destroy(collision.gameObject);
            }
        }
    }
}
