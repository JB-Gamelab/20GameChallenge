using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 4.5f;

    public static event Action <GhostController> OnGhostEaten;
    public static event Action OnPacmanEaten;
    private MovementController movementController;

    private MovementController.MoveDirection desiredDirection;

    private bool powerPillAcvtive = false;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        movementController.SetMoveSpeed(playerSpeed);
    }

    private void OnEnable()
    {
        movementController.OnDirectionChanged += MovementControllerOnDirectionChanged;
        GameManager.OnPowerPillCollected += GameManagerOnPowerPillCollected;
        GameManager.OnPowerPillExpired += GameManagerOnPowerPillExpired;
    }

    private void OnDisable()
    {
        movementController.OnDirectionChanged -= MovementControllerOnDirectionChanged;
        GameManager.OnPowerPillCollected -= GameManagerOnPowerPillCollected;
        GameManager.OnPowerPillExpired -= GameManagerOnPowerPillExpired;
    }

    private void MovementControllerOnDirectionChanged(MovementController.MoveDirection direction)
    {
        desiredDirection = direction;
    }

    private void Start()
    {
        desiredDirection = MovementController.MoveDirection.Stopped;
    }

    private void Update()
    {        
        movementController.Move(desiredDirection);
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredDirection = MovementController.MoveDirection.Right;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredDirection = MovementController.MoveDirection.Left;
        }
    }

    public void MoveUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredDirection = MovementController.MoveDirection.Up;
        }
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredDirection = MovementController.MoveDirection.Down;
        }
    }

    private void GameManagerOnPowerPillExpired()
    {
        powerPillAcvtive = false;
    }

    private void GameManagerOnPowerPillCollected()
    {
        powerPillAcvtive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ghost")
        {
            if (powerPillAcvtive)
            {
                OnGhostEaten?.Invoke(collision.GetComponent<GhostController>());
            }
            else
            {
                desiredDirection = MovementController.MoveDirection.Stopped;
                OnPacmanEaten?.Invoke();
            }
        }
    }
}
