using System;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject startText;
    [SerializeField] GameObject gameOverText;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Running)
        {
            this.gameObject.SetActive(false);
        }
        if (state == GameState.NotRunning)
        {
            this.gameObject.SetActive(true);
            startText.SetActive(true);
            gameOverText.SetActive(false);
        }
        if (state == GameState.GameOver)
        {
            this.gameObject.SetActive(true);
            startText.SetActive(false);
            gameOverText.SetActive(true);
        }
    }
}
