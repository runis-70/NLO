using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] private GameObject Health;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text TextNoInternet;
    private void Start()
    {
        LosePanel.SetActive(false);
    }
    public void OnVisible()
    {
        TextNoInternet.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(true);
        Health.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Lose()
    {
        LosePanel.SetActive(true);
    }
}
