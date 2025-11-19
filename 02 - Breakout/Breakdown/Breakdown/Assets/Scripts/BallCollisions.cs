using System;
using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private new Collider2D collider2D;
    private Ball ball;
    private Vector2 blockBottomLeft; //Block corner for side/top/bottom collision detection
    private Vector2 blockTopRight; //Block corner for side/top/bottom collision detection
    private float blockWidth = 1;
    private float blockHeight = 0.3f;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        ball = GetComponent<Ball>();                     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string frame = collision.gameObject.tag;
        switch (frame)
        {
        case ("Top"):
            ball.ReflectY();
            break;
        case ("Block"):            
            BlockSide(collider2D, collision);
            break;
        case ("Right"):
            ball.ReflectX();
            break;
        case ("Left"):
            ball.ReflectX();
            break;
        case ("Out"):
            gameManager.UpdateGameState(GameState.LifeLost);
            break;
        }
    }

    private void BlockSide(Collider2D thisCollider, Collider2D collision)
    {
        Vector2 blockCollisionPoint = collision.ClosestPoint(collision.bounds.center);
        Vector2 ballCollisionPoint = thisCollider.ClosestPoint(thisCollider.bounds.center);

        blockBottomLeft.x = blockCollisionPoint.x - (blockWidth / 2);
        blockBottomLeft.y = blockCollisionPoint.y - (blockHeight / 2);
        blockTopRight.x = blockCollisionPoint.x + (blockWidth / 2);
        blockTopRight.y = blockCollisionPoint.y + (blockHeight / 2);
        
        if (ballCollisionPoint.x > blockBottomLeft.x && ballCollisionPoint.x < blockTopRight.x)
        {
            ball.ReflectY();
        }
        if (ballCollisionPoint.y > blockBottomLeft.y && ballCollisionPoint.y < blockTopRight.y)
        {
            ball.ReflectX();
        }
    }
}
