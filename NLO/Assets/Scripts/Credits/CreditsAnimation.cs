using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAnimation : MonoBehaviour
{
   [SerializeField] private Credits creditsPanel;
   [SerializeField] private GameObject player;

   public void SetActive()
   {
        creditsPanel.gameObject.SetActive(false);
        player.SetActive(true);
    }
}
