using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action onPlayerDeath;

    private Rigidbody2D rB2D;

    private void Awake()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            onPlayerDeath?.Invoke();
            rB2D.linearVelocity = Vector2.zero;
            rB2D.angularVelocity = 0;
            gameObject.SetActive(false);
        }
    }
}
