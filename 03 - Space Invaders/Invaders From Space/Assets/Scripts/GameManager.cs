using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject enemyGroup;
    [SerializeField] private float respawnDelay = 1f;
    [SerializeField] private float levelLoadDelay = 3f;

    public int lives = 3;

    private float delay;

    public GameState currentState;

    private void Awake()
    {
        instance = this;
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
                // Play life lost SFX
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
                // Play GameOver SFX
                // Save score
                // Load high score table scene
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
    }

    public enum GameState
    {
        Running,
        LifeLost,
        LevelLoad,
        Gameover
    }
}
