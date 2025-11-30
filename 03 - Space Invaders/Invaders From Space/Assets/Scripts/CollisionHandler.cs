using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public static event Action onEnemyDestroyed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            BulletMovement bullet = collision.gameObject.GetComponent<BulletMovement>();
            if (bullet.GetWhoFired() == 1)
            {
                
                Destroy(gameObject);   
                onEnemyDestroyed?.Invoke();
            }            
        }
    }
}
