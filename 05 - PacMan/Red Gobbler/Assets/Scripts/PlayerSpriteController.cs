using System;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private MovementController movementController;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
    }

    private void OnEnable()
    {
        movementController.OnDirectionChanged += PlayerControllerOnDirectionChanged;        
    }

    private void OnDisable()
    {
        movementController.OnDirectionChanged -= PlayerControllerOnDirectionChanged;
    }

    private void Start()
    {
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void PlayerControllerOnDirectionChanged(MovementController.MoveDirection direction)
    {
        switch (direction)
        {
            case MovementController.MoveDirection.Right:
                playerSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, 0);
            break;
            case MovementController.MoveDirection.Left:
                playerSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, 180);
            break;
            case MovementController.MoveDirection.Up:
                playerSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, 90);
            break;
            case MovementController.MoveDirection.Down:
                playerSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, -90);
            break;
        }
    }
}
