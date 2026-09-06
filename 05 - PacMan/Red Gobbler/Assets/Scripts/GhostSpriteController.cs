using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.WSA;

public class GhostSpriteController : MonoBehaviour
{
    [SerializeField] private Sprite ghostSprite;
    [SerializeField] private Sprite eyeSprite;

    private SpriteRenderer ghostSpriteRenderer;
    private MovementController movementController;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
    }

    private void OnEnable()
    {
        movementController.OnDirectionChanged += MovementControllerOnDirectionChanged;
        PlayerController.OnGhostEaten += GhostEaten;
        GameManager.OnPowerPillCollected += GhostScared;
        GameManager.OnPowerPillExpired += GhostNormal;        
    }

    private void OnDisable()
    {
        movementController.OnDirectionChanged -= MovementControllerOnDirectionChanged;
        PlayerController.OnGhostEaten -= GhostEaten;
        GameManager.OnPowerPillCollected -= GhostScared;
        GameManager.OnPowerPillExpired -= GhostNormal;
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

    private void GhostEaten(GhostController ghost)
    {
        if (ghost == this.GetComponent<GhostController>())
        {
            ghostSpriteRenderer.sprite = eyeSprite;
        }
    }

    private void GhostScared()
    {
        //activate flash here
    }

    public void GhostNormal()
    {
        //deactivate flash here
        ghostSpriteRenderer.sprite = ghostSprite;
    }
}
