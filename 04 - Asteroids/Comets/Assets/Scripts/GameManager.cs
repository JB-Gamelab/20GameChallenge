using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action onGameOver;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private float respawnDelay = 1f;

    public int lives = 3;

    public GameState currentState;

    private void Awake()
    {
        instance = this;
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
        playerController.gameObject.transform.position = new Vector2(0, 0);
        playerController.gameObject.SetActive(true);
        
        UpdateGameState(GameState.Running);
    }

    private IEnumerator ReSpawnTimer()
    {
        yield return new WaitForSeconds(respawnDelay);
        Respawn();
    }
    
    public enum GameState
    {
        Running,
        LifeLost,
        Gameover
    }

}
