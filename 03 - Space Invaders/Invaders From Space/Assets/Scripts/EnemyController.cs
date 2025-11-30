using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static event Action<Transform> onEnemyDestroyed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            BulletMovement bullet = collision.gameObject.GetComponent<BulletMovement>();
            if (bullet.GetWhoFired() == 1)
            {                   
                onEnemyDestroyed?.Invoke(transform);
                gameObject.SetActive(false);
            }            
        }
    }
}
