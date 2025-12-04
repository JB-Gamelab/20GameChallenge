using System;
using Unity.Collections;
using UnityEditor.Build.Reporting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action onGameOver;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject enemyGroup;
    [SerializeField] private float respawnDelay = 1f;
    [SerializeField] private float levelLoadDelay = 3f;
    private ScoreManager scoreManager;

    public int lives = 3;
    public int level = 1;

    private float delay;

    public GameState currentState;

    private void Awake()
    {
        instance = this;
        scoreManager = GetComponent<ScoreManager>();
        UpdateGameState(GameState.Running);
    }

    private void Update()
    {
        if (currentState == GameState.LifeLost)
        {
            Time.timeScale = 0f;
            
            if (delay < respawnDelay)
            {
                delay = delay + Time.unscaledDeltaTime;
            }
            else
            {
                Respawn();
            }
        }
        if (currentState == GameState.LevelLoad)
        {
            Time.timeScale = 0f;
            
            if (delay < levelLoadDelay)
            {
                delay = delay + Time.unscaledDeltaTime;
            }
            else
            {
                LevelLoad();
            }
        }
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;

        switch(newState)
        {
            case GameState.Running:
                // Play BG music
                Time.timeScale = 1f;
                return;

            case GameState.LifeLost:
                LoseLife();
                if (lives == 0)
                {
                    UpdateGameState(GameState.Gameover);
                }
                else
                {
                    delay = 0f;
                }
                
                return;

            case GameState.LevelLoad:
                return;

            case GameState.Gameover:
                onGameOver?.Invoke();
                PlayerPrefsManager.SetLastGameScore(scoreManager.score);
                SceneManager.LoadScene(2);
                Time.timeScale = 0f;
                return;
        }
    }

    private int LoseLife()
    {
        lives--;
        return lives;
    }

    private void Respawn()
    {
        playerController.gameObject.SetActive(true);
        
        UpdateGameState(GameState.Running);
    }
    
    private void LevelLoad()
    {
        enemyGroup.transform.position = new Vector3(0, 0, 0); // reset enemy group back to start position

        for (int i = 0; i < enemyGroup.transform.childCount; i++)
        {
            Transform enemy = enemyGroup.transform.GetChild(i);

            enemy.gameObject.SetActive(true);
        }
        
        UpdateGameState(GameState.Running);
        level++;
    }

    public enum GameState
    {
        Running,
        LifeLost,
        LevelLoad,
        Gameover
    }
}
