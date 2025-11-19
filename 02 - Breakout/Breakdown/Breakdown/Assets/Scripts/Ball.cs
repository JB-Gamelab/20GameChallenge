using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 10f; //speed of the ball
    [SerializeField] private Vector2 startDir = new Vector2(2,2); //start direction of the ball
    [SerializeField] private GameObject paddle;
    [SerializeField] private GameManager gameManager;

    private new Collider2D collider2D;
    private Vector2 blockBottomLeft;
    private Vector2 blockTopRight;
    private float blockWidth = 1;
    private float blockHeight = 0.3f;

    private Vector2 direction; //ball direction

    private bool running = false;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Running)
        {
            running = true;  
            direction = startDir;            
        } 
        else if (state == GameState.LifeLost)
        {
            running = false;
            transform.position = new Vector2(0, -2.7f);
        }
        else
        {
            running = false;
        }
    }

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();                     
    }

    private void Update()
    {        
        if (running)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string frame = collision.gameObject.tag;
        switch (frame)
        {
        case ("Top"):
            ReflectY();
            break;
        case ("Block"):            
            BlockSide(collider2D, collision);
            break;
        case ("Right"):
            ReflectX();
            break;
        case ("Left"):
            ReflectX();
            break;
        case ("Out"):
            gameManager.UpdateGameState(GameState.LifeLost);
            break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Paddle")
        {
            float relativeX = (this.transform.position.x - paddle.transform.position.x) / (1.4f / 2);
            direction = new Vector2(relativeX, direction.y * -1);
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
            ReflectY();
        }
        if (ballCollisionPoint.y > blockBottomLeft.y && ballCollisionPoint.y < blockTopRight.y)
        {
            ReflectX();
        }
    }

    private void ReflectX()
    {
        direction = new Vector2(direction.x * -1, direction.y);
    }

    private void ReflectY()
    {
        direction = new Vector2(direction.x, direction.y * -1);
    }
}
