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

    private TMP_Text scoreTextMeshPro;
    private TMP_Text livesTextMeshPro;
    private bool running = false;
    private bool gameOver = false;

    private int score = 0;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
        Block.onBlockDestoyed += BlockOnBlockDestroyed;
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
        livesTextMeshPro.text = "Lives: " + gameManager.GetLives();
        scoreTextMeshPro.text = "Score: " + score;
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
        }
    }
}
