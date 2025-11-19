using UnityEngine;
using UnityEngine.UIElements;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;
        GameOver();
    }

    private void Update()
    {
        if (this.transform.position.y > 4.7 || this.transform.position.y < -4.7)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameManager.UpdateGameState(GameState.GameOver);
    }
}
