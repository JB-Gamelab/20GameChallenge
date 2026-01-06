using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MovementController movementController;

    private MovementController.MoveDirection desiredDirection;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
    }

    private void OnEnable()
    {
        movementController.OnDirectionChanged += MovementControllerOnDirectionChanged;
    }

    private void OnDisable()
    {
        movementController.OnDirectionChanged -= MovementControllerOnDirectionChanged;
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

    
}
