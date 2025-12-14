using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image life1Image;
    [SerializeField] Image life2Image;
    [SerializeField] Image life3Image;


    private void Awake()
    {
        ScoreManager.onScoreChanged += ScoreManagerOnScoreChanged;
        GameManager.onLifeLost += GameManagerOnLifeLost;
    }

    private void OnDestroy()
    {
        ScoreManager.onScoreChanged -= ScoreManagerOnScoreChanged;
        GameManager.onLifeLost -= GameManagerOnLifeLost;
    }

    private void Start()
    {
        scoreText.text = "SCORE: 0";
        life1Image.gameObject.SetActive(true);
        life2Image.gameObject.SetActive(true);
        life3Image.gameObject.SetActive(true);
    }

    private void GameManagerOnLifeLost(int lives)
    {
        if (lives == 3)
        {
            life1Image.gameObject.SetActive(true);
            life2Image.gameObject.SetActive(true);
            life3Image.gameObject.SetActive(true);
        }
        if (lives == 2)
        {
            life1Image.gameObject.SetActive(true);
            life2Image.gameObject.SetActive(true);
            life3Image.gameObject.SetActive(false);
        }
        if (lives == 1)
        {
            life1Image.gameObject.SetActive(true);
            life2Image.gameObject.SetActive(false);
            life3Image.gameObject.SetActive(false);
        }
        if (lives == 0)
        {
            life1Image.gameObject.SetActive(false);
            life2Image.gameObject.SetActive(false);
            life3Image.gameObject.SetActive(false);
        }
    }

    private void ScoreManagerOnScoreChanged(int score)
    {
        scoreText.text = "SCORE: " + score;
    }
}
