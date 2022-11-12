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
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InstantiateMobsManager instantiateMobsManager;
    [SerializeField] private InstantiateBossManager instantiateBossManager;
    [SerializeField] private FadeScene fadeScene;


    [Header("Рекорды")]
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text CurrentScoreText;
    [SerializeField] private Text BestScoreText;
    [SerializeField] private Image pausePanel;
    [SerializeField] private Image settingsPanel;
    [SerializeField] private string nameKey;
    [Header("Настройка системы подсчета убийств")]
    [SerializeField] private Slider CountMurderSlider;
    [SerializeField] private Text CountMurderText;
    [SerializeField] private Image FillCountMurderSlider;
    [Header("Настройка системы боссов")]
    [SerializeField] private Slider BossHealthSlider;
    [SerializeField] private BossText BossText;

    private int score;
    private int BestScore;
    private int maxHP;
    private int maxCountMurders;
    private int countMurders;
    private bool isBossCall = false;

    private void Awake()
    {
        Input.backButtonLeavesApp = false;
        maxCountMurders = playerController.GetMaxCountMurders();
        CountMurderSlider.maxValue = maxCountMurders;
        PlayerController.CountMurders += UpdateMurdersInfo;
    }
    private void Start()
    {
        BestScore = SaveManager.LoadInt(nameKey);
        PlayerController.Score += UpdateScoreText;

        maxHP = playerController.GetMaxHP();
        FillCountMurderSlider.gameObject.SetActive(false);
        fadeScene.CloseSceneAnim();
    }
    private void Update()
    {
        UpdateHealthImage();
        TextOfBossCall();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerController.enabled = false;
            pausePanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        if (countMurders > 0)
            FillCountMurderSlider.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        PlayerController.Score -= UpdateScoreText;
        PlayerController.CountMurders -= UpdateMurdersInfo;
    }
    private void OnBestScore()
    {
        if (score > BestScore)
        {
            SaveManager.Save(nameKey, score);
            BestScoreText.text = score.ToString();
        }
        else
            BestScoreText.text = SaveManager.LoadInt(nameKey).ToString();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void UpdateScoreText(int score)
    {
        this.score += score;
        ScoreText.text = ScoreText.text.Remove(ScoreText.text.ToString().Length - this.score.ToString().Length);
        print(this.score.ToString().Length);
        ScoreText.text += this.score.ToString();
    }
    private void UpdateMurdersInfo(int murders)
    {
        countMurders += murders;
        CountMurderSlider.value = countMurders;
        CountMurderText.text = countMurders + "/" + maxCountMurders;
    }
    private void UpdateHealthImage()
    {
        for (int i = 0; i < healthArray.Length; i++)
        {
            if (i < playerController.GetHP())
            {
                healthArray[i].GetComponent<Animator>().SetInteger("Health", 1);
            }
            else
            {
                healthArray[i].GetComponent<Animator>().SetInteger("Health", 2);
            }
        }
    }
    private void TextOfBossCall()
    {
        if (countMurders == maxCountMurders & isBossCall == false)
        {
            isBossCall = true;
            BossHealthSlider.gameObject.SetActive(true);
            CountMurderSlider.gameObject.SetActive(false);
            BossText.SelectTextBossCall();
            instantiateMobsManager.StopAllCoroutines();
        }
    }
    public void BossNameCall()
    {
        BossText.SelectTextNameBoss(instantiateBossManager.GetNameBoss());
    }
    public void HomeButton()
    {
        Time.timeScale = 1f;
        pausePanel.gameObject.SetActive(false);
        fadeScene.OpenSceneAnim();
    }
    public void ContinueButton()
    {
        playerController.enabled = true;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SettingsButton()
    {
        playerController.enabled = false;
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
        CurrentScoreText.text = score.ToString();
    }
    public int GetCountMurders()
    {
        return countMurders;
    }
    public int GetMaxCountMurders()
    {
        return maxCountMurders;
    }
}
