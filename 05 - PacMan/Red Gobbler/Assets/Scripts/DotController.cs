using System;
using UnityEngine;

public class DotController : MonoBehaviour
{
    public static event Action OnDotCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnDotCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
