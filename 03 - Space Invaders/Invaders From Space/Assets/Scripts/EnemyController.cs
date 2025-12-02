using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int value = 100;
    public static event Action<Transform> onEnemyDestroyed;
    public static event Action<int> onEnemyScored;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            BulletMovement bullet = collision.gameObject.GetComponent<BulletMovement>();
            if (bullet.GetWhoFired() == 1)
            {                   
                onEnemyDestroyed?.Invoke(transform);
                onEnemyScored?.Invoke(value);
                gameObject.SetActive(false);
            }            
        }
    }
}
