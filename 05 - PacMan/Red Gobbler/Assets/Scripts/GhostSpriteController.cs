using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.WSA;

public class GhostSpriteController : MonoBehaviour
{
    [SerializeField] private Sprite ghostSprite;
    [SerializeField] private Sprite eyeSprite;
    [SerializeField] private Sprite scaredSprite;

    private SpriteRenderer ghostSpriteRenderer;
    private MovementController movementController;
    private GhostBehaviour ghostBehaviour;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        ghostBehaviour = GetComponent<GhostBehaviour>();
        ghostSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        movementController.OnDirectionChanged += MovementControllerOnDirectionChanged;
        ghostBehaviour.OnStateChanged += GhostStateChange;
    }

    private void OnDisable()
    {
        movementController.OnDirectionChanged -= MovementControllerOnDirectionChanged;
        ghostBehaviour.OnStateChanged -= GhostStateChange;
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

    private void GhostEaten(GhostController ghost)
    {
        if (ghost == this.GetComponent<GhostController>())
        {
            ghostSpriteRenderer.sprite = eyeSprite;
        }
    }

    private void GhostStateChange(GhostBehaviour.GhostState ghostState)
    {
        if (ghostState == GhostBehaviour.GhostState.Eaten)
        {
            ghostSpriteRenderer.sprite = eyeSprite;
        } 
        else if (ghostState == GhostBehaviour.GhostState.Scared)
        {
            ghostSpriteRenderer.sprite = scaredSprite;
        }
        else
        {
            ghostSpriteRenderer.sprite = ghostSprite;
        }
    }
}
