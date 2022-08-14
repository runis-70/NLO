using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    // Функции для кнопок
    public void PlayButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void DisablePanel(Image panel)
    {
        panel.gameObject.SetActive(true);
    }
}
