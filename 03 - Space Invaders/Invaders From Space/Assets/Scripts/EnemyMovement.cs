using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveInterval = 5f; //Time between move actions
    [SerializeField] private float maxXPos = 8f; //Sets max +/- X position for enemy movement
    [SerializeField] private EnemyPositions enemyPositions;
    private bool movingRight = true; //Sets the move direction
    private bool dropNext = false; //Sets next move action down

    private float moveDelay = 0;
    private float startLeftMostX = 8;
    private float startRightMostX = -8;
    private float leftMostX; //X position of leftmost enemy sprite
    private float rightMostX; //X position of rightmost enemy sprite

    public void Awake()
    {
        CollisionHandler.onEnemyDestroyed += CollisionHandlerOnEnemyDestroyed;
    }

    private void CollisionHandlerOnEnemyDestroyed()
    {
        enemyPositions.lowestPerColumn.Clear();
        enemyPositions.enemyCanShootList.Clear();
        leftMostX = enemyPositions.CalculateEnemyPositions(startLeftMostX, startRightMostX)[0];
        rightMostX = enemyPositions.CalculateEnemyPositions(startLeftMostX, startRightMostX)[1];
    }

    private void Start()
    {
        leftMostX = enemyPositions.CalculateEnemyPositions(startLeftMostX, startRightMostX)[0];
        rightMostX = enemyPositions.CalculateEnemyPositions(startLeftMostX, startRightMostX)[1];
    }

    private void Update()
    {
        moveDelay = moveDelay + Time.deltaTime;

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
            leftMostX = enemyPositions.CalculateEnemyPositions(startLeftMostX, startRightMostX)[0];
            rightMostX = enemyPositions.CalculateEnemyPositions(startLeftMostX, startRightMostX)[1];
            Debug.Log(leftMostX + " " + rightMostX);
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

    
}
