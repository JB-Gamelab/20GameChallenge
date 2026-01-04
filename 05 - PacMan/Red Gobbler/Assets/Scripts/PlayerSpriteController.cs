using System;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        PlayerController.OnDirectionChanged += PlayerControllerOnDirectionChanged;
    }

    private void OnDestroy()
    {
        PlayerController.OnDirectionChanged -= PlayerControllerOnDirectionChanged;
    }

    private void Start()
    {
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void PlayerControllerOnDirectionChanged(PlayerController.MoveDirection direction)
    {
        switch (direction)
        {
            case PlayerController.MoveDirection.Right:
                playerSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, 0);
            break;
            case PlayerController.MoveDirection.Left:
                playerSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, 180);
            break;
            case PlayerController.MoveDirection.Up:
                playerSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, 90);
            break;
            case PlayerController.MoveDirection.Down:
                playerSpriteRenderer.transform.eulerAngles = new Vector3(0, 0, -90);
            break;
        }
    }
}
