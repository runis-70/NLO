using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SaveManagerLibrary;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image[] healthArray;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private Controller controller;
    [SerializeField] private FadeScene fadeScene;
    [SerializeField] private string nameKey;
    

    [Header("Рекорды")]
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text CurrentScoreText;
    [SerializeField] private Text BestScoreText;
    [SerializeField] private Image pausePanel;
    [SerializeField] private Image settingsPanel;
    [SerializeField] private DontDestroy dontDestroy;
    [Header("Настройка системы подсчета убийств")]
    [SerializeField] private Slider CountMurderSlider;
    [SerializeField] private Text CountMurderText;
    [SerializeField] private Image FillCountMurderSlider;

    private int Score;
    private int BestScore;
    private int maxHP;
    private int maxCountMurder = 150;

    public static Action BossCalled;

    private void Start()
    {
        Input.backButtonLeavesApp = false;
        fadeScene.CloseSceneAnim();
        BestScore = SaveManager.LoadInt(nameKey);
        Controller.Score += UpdateScoreText;
        maxHP = controller.GetMaxHP();
        CountMurderSlider.maxValue = maxCountMurder;
        FillCountMurderSlider.gameObject.SetActive(false);
        UpdateCountMurderText();
    }
    private void Update()
    {
        CountMurderSlider.value = controller.GetNumberOfMurders();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controller.enabled = false;
            pausePanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
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
        if (controller.GetNumberOfMurders() > 0)
            FillCountMurderSlider.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        Controller.Score -= UpdateScoreText;
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
    public void UpdateScoreText(int score)
    {
        Score += score;
        ScoreText.text = ScoreText.text.Remove(ScoreText.text.ToString().Length - Score.ToString().Length);
        print(Score.ToString().Length);
        ScoreText.text += Score.ToString();
    }
    public void UpdateCountMurderText()
    {
        CountMurderText.text = controller.GetNumberOfMurders() + "/" + maxCountMurder;
        if (controller.GetNumberOfMurders() == maxCountMurder)
            BossCalled.Invoke();
    }
    public void HomeButton()
    {
        Time.timeScale = 1f;
        pausePanel.gameObject.SetActive(false);
        fadeScene.OpenSceneAnim();
    }
    public void ContinueButton()
    {
        controller.enabled = true;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SettingsButton()
    {
        controller.enabled = false;
        settingsPanel.gameObject.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(false);
    }
    public void Lose()
    {
        LosePanel.SetActive(true);
        OnBestScore();  
        CurrentScoreText.text = Score.ToString();
    }
}
