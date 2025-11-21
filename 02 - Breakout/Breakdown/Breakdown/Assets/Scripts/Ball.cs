using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 10f; //speed of the ball
    [SerializeField] private Vector2 startDir = new Vector2(2,2); //start direction of the ball
    [SerializeField] private GameObject paddle;
    [SerializeField] private AudioClip bounce;
    
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

    private void Update()
    {        
        if (running)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Paddle")
        {
            AudioSource.PlayClipAtPoint(bounce, transform.position, 1.0f);
            float relativeX = (this.transform.position.x - paddle.transform.position.x) / (1.4f / 2);
            direction = new Vector2(relativeX, direction.y * -1);
        }
    }    

    public void ReflectX()
    {
        direction = new Vector2(direction.x * -1, direction.y);
    }

    public void ReflectY()
    {
        direction = new Vector2(direction.x, direction.y * -1);
    }
}
