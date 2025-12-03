using System;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite alienSprite1;
    [SerializeField] private Sprite alienSprite2;

    private SpriteRenderer spriteRenderer;

    private int turn = 1;

    private void Awake()
    {
        EnemyMovement.onMoveAction += EnemyMovementOnMoveAction;
    }

    private void OnDestroy()
    {
        EnemyMovement.onMoveAction -= EnemyMovementOnMoveAction;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void EnemyMovementOnMoveAction()
    {
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if(turn == 1)
        {
            spriteRenderer.sprite = alienSprite2;
            turn = 2;
        }
        else if(turn == 2)
        {
            spriteRenderer.sprite = alienSprite1;
            turn = 1;
        }
    }
}
