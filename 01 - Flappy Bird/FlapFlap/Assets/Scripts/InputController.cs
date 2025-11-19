using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private FlapFlap actions;

    [SerializeField] private float flapSpeed = 1f;

    private new Rigidbody2D rigidbody2D;

    private bool running;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
        actions = new FlapFlap();
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
        if (context.performed)
        {
            rigidbody2D.linearVelocityY = flapSpeed;
        }
    }

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!running)
        {
            rigidbody2D.simulated = false;
        }
        else
        {
            rigidbody2D.simulated = true;
        }
    }
}
