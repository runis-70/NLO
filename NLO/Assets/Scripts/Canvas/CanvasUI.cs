using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SaveManagerLibrary;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] private GameObject Health;
    [SerializeField] private Image[] healthArray;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private Controller controller;
    [SerializeField] private string nameKey;
    

    [Header("Рекорды")]
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text CurrentScoreText;
    [SerializeField] private Text BestScoreText;

    private int Score;
    private int BestScore;
    private int maxHP;

    private void Start()
    {
        BestScore = SaveManager.LoadInt(nameKey);
        Controller.Score += UpdateText;
        maxHP = controller.GetMaxHP();
    }
    private void Update()
    {
        for (int i = 0; i < healthArray.Length; i++)
        {
            if (i < controller.GetHP())
            {
                healthArray[i].enabled = true;
            }
            else
            {
                healthArray[i].enabled = false;
            }
        }
    }
    private void OnDisable()
    {
        Controller.Score -= UpdateText;
    }
    private void OnBestScore()
    {
        if (Score > BestScore)
        {
            SaveManager.Save(nameKey, Score);
            BestScoreText.text = Score.ToString();
        }
        else
            BestScoreText.text = SaveManager.LoadInt(nameKey).ToString();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UpdateText(int score)
    {
        ScoreText.text = score.ToString();
        Score = score;
    }
    public void Lose()
    {
        LosePanel.SetActive(true);
        OnBestScore();  
        CurrentScoreText.text = Score.ToString();
    }
}
