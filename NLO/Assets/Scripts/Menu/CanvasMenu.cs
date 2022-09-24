using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private MusicManager musicManager;
    public static Action WayStarted;

    private void Start()
    {
        Input.backButtonLeavesApp = true;
    }
    // Функции для кнопок
    public void PlayButton()
    {
        WayStarted?.Invoke();
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void DisablePanel(Image panel)
    {
        panel.gameObject.SetActive(false);
        player.SetActive(true);
    }
    public void EnablePanel(Image panel)
    {
        panel.gameObject.SetActive(true);
        player.SetActive(false);
    }
}
