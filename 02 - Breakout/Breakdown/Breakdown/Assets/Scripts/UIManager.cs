using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject livesText;
    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject hiScoreText;

    private TMP_Text scoreTextMeshPro;
    private TMP_Text livesTextMeshPro;
    private TMP_Text levelTextMeshPro;
    private TMP_Text hiScoreTextMeshPro;
    private bool running = false;
    private bool gameOver = false;

    private int score = 0;
    private int level = 1;
    private int hiScore;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
        Block.onBlockDestoyed += BlockOnBlockDestroyed;
        BlockHandler.onLevelIncrease += BlockHandlerOnLevelIncrease;
        hiScore = PlayerPrefsManager.LoadScore();
    }

    private void BlockHandlerOnLevelIncrease(int obj)
    {
        level++;
        levelTextMeshPro.text = "Level: " + level;
    }

    private void BlockOnBlockDestroyed(int obj)
    {
        score = score + obj;
        scoreTextMeshPro.text = "Score: " + score;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Running)
        {
            running = true;
            gameOver = false;          
        } 
        if (state == GameState.GameOver)
        {
            running = false;
            gameOver = true;
            level = 1;
        }
        if (state == GameState.LifeLost)
        {
            livesTextMeshPro.text = "Lives: " + gameManager.GetLives();
        }
    }

    private void Start()
    {
        livesTextMeshPro = livesText.GetComponent<TMP_Text>();
        scoreTextMeshPro = scoreText.GetComponent<TMP_Text>();
        levelTextMeshPro = levelText.GetComponent<TMP_Text>();
        hiScoreTextMeshPro = hiScoreText.GetComponent<TMP_Text>();
        livesTextMeshPro.text = "Lives: " + gameManager.GetLives();
        scoreTextMeshPro.text = "Score: " + score;
        levelTextMeshPro.text = "Level: " + level;
        hiScoreTextMeshPro.text = "Best: " + hiScore;
    }

    private void Update()
    {
        if (!running && !gameOver)
        {
            startText.SetActive(true);
            gameOverText.SetActive(false);
        } else
        if (running)
        {
            startText.SetActive(false);
            gameOverText.SetActive(false);
        } else
        if (gameOver)
        {
            startText.SetActive(false);
            gameOverText.SetActive(true);
            if (score > hiScore)
            {
                PlayerPrefs.SetInt("PlayerScore", score);
            }
        }
    }
}
