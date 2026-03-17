using System;
using UnityEngine;

public class PowerPillController : MonoBehaviour
{
    public static event Action OnPillCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnPillCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
