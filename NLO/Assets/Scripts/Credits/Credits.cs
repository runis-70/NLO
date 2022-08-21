using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private Image creditsPanel;
    [SerializeField] private GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            creditsPanel.gameObject.SetActive(false);
            player.SetActive(true);
        }
    }
}
