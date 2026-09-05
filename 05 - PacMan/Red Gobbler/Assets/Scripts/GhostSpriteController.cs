using System;
using UnityEngine;

public class GhostSpriteController : MonoBehaviour
{
    private SpriteRenderer ghostSpriteRenderer;
    private MovementController movementController;

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

    private void Start()
    {
        ghostSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void MovementControllerOnDirectionChanged(MovementController.MoveDirection direction)
    {
        switch (direction)
        {
            case MovementController.MoveDirection.Right:
                ghostSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, 0);
            break;
            case MovementController.MoveDirection.Left:
                ghostSpriteRenderer.transform.eulerAngles = new Vector3(0, 180, 0);
            break;
        }
    }
}
