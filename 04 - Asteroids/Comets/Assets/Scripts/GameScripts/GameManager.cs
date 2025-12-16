using System;
using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action onGameOver;
    public static event Action<int> onLifeLost;
    public static event Action onRespawn;
    public static event Action<bool> onPause;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private float respawnDelay = 5f;

    public int lives = 3;

    public GameState currentState;
    private bool pauseState;
    private ScoreManager scoreManager;

    private void Awake()
    {
        instance = this;
        scoreManager = GetComponent<ScoreManager>();
        UpdateGameState(GameState.Running);
        PlayerController.onPlayerDeath += PlayerControllerOnPlayerDeath;
    }

    private void OnDestroy()
    {
        PlayerController.onPlayerDeath -= PlayerControllerOnPlayerDeath;
    }

    private void PlayerControllerOnPlayerDeath()
    {
        UpdateGameState(GameState.LifeLost);
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;

        switch(newState)
        {
            case GameState.Running:
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
                    StartCoroutine(ReSpawnTimer());
                }                
                return;

            case GameState.Gameover:
                onGameOver?.Invoke();
                Time.timeScale = 0f;
                PlayerPrefsManager.SetLastGameScore(scoreManager.GetScore());
                SceneManager.LoadScene(2);
                return;

            case GameState.Paused:
                Time.timeScale = 0f;
                return;
        }
    }

    private int LoseLife()
    {
        lives--;
        onLifeLost?.Invoke(lives);
        return lives;
    }

    private void Respawn()
    {
        playerController.gameObject.transform.position = new Vector2(0, 0);
        playerController.gameObject.SetActive(true);

        onRespawn?.Invoke();
        
        UpdateGameState(GameState.Running);
    }

    private IEnumerator ReSpawnTimer()
    {
        yield return new WaitForSeconds(respawnDelay);
        Respawn();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Pause();            
        }
    }

    public void Pause()
    {
        if (currentState == GameState.Running)
            {
                UpdateGameState(GameState.Paused);
                pauseState = true;
            }
            else if (currentState == GameState.Paused)
            {
                UpdateGameState(GameState.Running);
                pauseState = false;
            }

            onPause?.Invoke(pauseState);
    }

    public void OnQuit()
    {
        SceneManager.LoadScene(0);
    }
    
    public enum GameState
    {
        Running,
        LifeLost,
        Gameover,
        Paused
    }

}
