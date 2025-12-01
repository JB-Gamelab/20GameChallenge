using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveMod = 5f;
    [SerializeField] private float maxXPos = 8f; //Sets max +/- X position for enemy movement
    [SerializeField] private EnemyPositions enemyPositions;

    private bool movingRight = true; //Sets the move direction
    private bool dropNext = false; //Sets next move action down

    private float moveDelay = 0;
    private float leftMostX; //X position of leftmost enemy sprite
    private float rightMostX; //X position of rightmost enemy sprite
    private float moveInterval;

    private void Awake()
    {
        EnemyManager.onEnemyCountChanged += EnemyManagerOnEnemyCountChanged;
    }

    private void Update()
    {
        moveDelay = moveDelay + Time.deltaTime;

        leftMostX = enemyPositions.leftMostX;
        rightMostX = enemyPositions.rightMostX;

        if (moveDelay > moveInterval)
        {
            MoveAction();
            moveDelay = 0;   
        }
    }

    private void MoveAction()
    {
        if (dropNext)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            dropNext = false;
        } 
        else
        {
            if (movingRight)
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }
            if (!movingRight)
            {
                transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            }
            
            CheckNextAction();
        }
        
    }

    private void CheckNextAction()
    {
        if (rightMostX + transform.position.x == maxXPos)
        {
            dropNext = true;
            movingRight = false;
        }
        if (leftMostX + transform.position.x == maxXPos * -1)
        {
            dropNext = true;
            movingRight = true;
        }
    }

    private void EnemyManagerOnEnemyCountChanged(int enemyCount)
    {
        moveInterval = enemyCount / moveMod;
    }    
}
