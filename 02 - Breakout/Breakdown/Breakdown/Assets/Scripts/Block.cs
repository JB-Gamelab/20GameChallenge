using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    public static event Action<int> onBlockDestoyed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioClip breakSound;
    private int score = 25;

    private void Start()
    {
        int randCol = UnityEngine.Random.Range(0, 5);

        switch (randCol)
        {
            case (0):
                spriteRenderer.color = Color.blue;
                break;
            case (1):
                spriteRenderer.color = Color.red;
                break;   
            case (2):
                spriteRenderer.color = Color.green;
                break;
            case (3):
                spriteRenderer.color = Color.yellow;
                break;
            case (4):
                spriteRenderer.color = Color.purple;
                break;
            case (5):
                spriteRenderer.color = Color.orange;
                break;
        }
          
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            Instantiate(particles, transform.position, quaternion.identity);
            AudioSource.PlayClipAtPoint(breakSound, transform.position, 1.0f);
            onBlockDestoyed?.Invoke(score);            
            Destroy(gameObject);            
        }
    }
}
