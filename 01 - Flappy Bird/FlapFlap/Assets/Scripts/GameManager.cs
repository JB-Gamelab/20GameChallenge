using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    public static event Action<GameState> onGameStateChanged;

    private FlapFlap actions;

    private bool notRunning;
    private bool gameOver;

    void Awake()
    {
        instance = this;

        actions = new FlapFlap();
    }

    void Start()
    {
        UpdateGameState(GameState.NotRunning);
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Flap.performed += Flap;

        actions.Player.Flap.canceled += Flap;
    }

    private void OnDisable()
    {
        actions.Player.Disable();
        actions.Player.Flap.performed -= Flap;
    }

    private void Flap(InputAction.CallbackContext context)
    {
        if (context.performed && notRunning)
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
                gameOver = false;
                break;
            case GameState.Running:
                notRunning = false;
                gameOver = false;
                break;
            case GameState.GameOver:
                notRunning = false;
                gameOver = true;
                break;
        }

        onGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    NotRunning,
    Running,
    GameOver
}
