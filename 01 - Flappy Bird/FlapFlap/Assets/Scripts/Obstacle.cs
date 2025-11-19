using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    [SerializeField] private bool running = true;

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
        } else
        {
            running = false;
        }
    }

    void Update()
    {
        if (running)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (transform.position.x < -12)
        {
            Destroy(this.gameObject);
        }
    }
}
