using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] private GameObject Health;
    [SerializeField] private Image[] healthArray;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text TextNoInternet;
    [SerializeField] private Controller controller;
    [SerializeField] private GenerateObject generateObject;
    private float Score;
    private int maxHP;
    private void Start()
    {
        Controller.YouLose += Lose;
        Controller.Score += UpdateText;
        maxHP = controller.GetMaxHP();
        LosePanel.SetActive(false);
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
        Controller.YouLose -= Lose;
        Controller.Score -= UpdateText;
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
    public void UpdateText(float score)
    {
        Score += score;
        ScoreText.text = Score.ToString();
        finalScoreText.text = ScoreText.text;
    }
    public void Lose()
    {
        LosePanel.SetActive(true);
    }
}
