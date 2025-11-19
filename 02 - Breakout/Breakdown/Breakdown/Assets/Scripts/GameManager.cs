using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;

    public static event Action<GameState> onGameStateChanged;

    [SerializeField] private Vector2[] gridPos; // Array of possible block spawn locations
    [SerializeField] private bool[] blockPos; // Array of bools same size as gridPos, if array location true then spawn block at that gridPos
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int lives = 3;

    private Controls inputActions;    
    private bool notRunning;
    private bool lifeLost;
    private bool gameOver;

    private void Awake()
    {
        Instance = this;
        inputActions = new Controls();
    }

    private void Start()
    {
        UpdateGameState(GameState.NotRunning);

        for (int i = 0; i < gridPos.Length; i++)
        {
            if (blockPos[i])
            {
                Vector3 position = new Vector3(gridPos[i].x, gridPos[i].y, 0);
                Instantiate(blockPrefab, position, Quaternion.identity);
            }
        }
    }

    private void OnEnable()
    {
        inputActions.Controller.Enable();
        
        inputActions.Controller.Confirm.performed += StartGame;
        inputActions.Controller.Confirm.canceled += StartGame;
    }

    private void OnDisable()
    {
        inputActions.Controller.Disable();
        inputActions.Controller.Confirm.performed -= StartGame;
    }

    private void StartGame(InputAction.CallbackContext context)
    {
        if ((context.performed && notRunning) || (context.performed && lifeLost))
        {
            UpdateGameState(GameState.Running);
        }
        if (context.performed && gameOver)
        {
            SceneManager.LoadScene(0);
        }
    }

        public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.NotRunning:
                notRunning = true;
                lifeLost = false;
                gameOver = false;
                break;
            case GameState.Running:                
                notRunning = false;
                lifeLost = false;
                gameOver = false;
                break;
            case GameState.LifeLost:
                notRunning = false;
                lifeLost = true;
                gameOver = false;
                LoseLife();
                if (lives == 0)
                {
                    UpdateGameState(GameState.GameOver);
                }
                break;
            case GameState.GameOver:
                notRunning = false;
                lifeLost = false;
                gameOver = true;
                break;
        }

        onGameStateChanged?.Invoke(newState);
    }

    private void LoseLife()
    {
        lives--;
        
    }

    public int GetLives()
    {
        return lives;
    }
}

public enum GameState
{
    NotRunning,
    Running,
    LifeLost,
    GameOver
}

