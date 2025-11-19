using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    [SerializeField] float yAxisPos = -3f; //fixes y position of paddle
    [SerializeField] float xAxisCap = 4.8f; // caps x position of paddle

    private bool running = false;

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
            running = true;            
        } 
        if (state == GameState.LifeLost)
        {
            running = false;
            transform.position = new Vector2(0, -3);
        }
        if (state == GameState.GameOver)
        {
            running = false;
        }
    }

    private void Update()
    {
        if (running)
        {
            PaddleMove();
        }
    }

    private void PaddleMove()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 screenPosition = gameCamera.ScreenToWorldPoint(mousePosition);
        float xPos;
        if (screenPosition.x < xAxisCap * -1)
        {
            xPos = xAxisCap * -1;        
        } else if (screenPosition.x > xAxisCap)
        {
            xPos = xAxisCap;
        } else
        {
            xPos = screenPosition.x;
        }
        this.transform.position = new Vector2(xPos, yAxisPos);
    }
}
