using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] private bool isGame = false;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sound");    

        if (objs.Length > 1)
        {
            for (int i = 0; i < objs.Length - 1; i++)
            {
                Destroy(objs[i].gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game" && isGame)
        {
            this.gameObject.SetActive(false);
        }
    }
}
