using System;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public static event Action<bool> onBulletDestroyed;
    [SerializeField] float speed = 5f;

    public MonoBehaviour owner;
    private bool playersBullet = false;

    private void Start()
    {
        SpriteRenderer bulletSprite = GetComponent<SpriteRenderer>();

        if (owner is PlayerController)
        {
            bulletSprite.color = Color.greenYellow;
            playersBullet = true;
        }
        if (owner is EnemyFire)
        {
            bulletSprite.color = Color.orange;
        }
    }

    private void Update()
    {
        if (owner is PlayerController)
        {
            transform.position += Time.deltaTime * Vector3.up * speed;
        }
        if (owner is EnemyFire)
        {
            transform.position += Time.deltaTime * Vector3.down * speed;
        }
    }

    public int GetWhoFired()
    {
        if (owner is PlayerController)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner is PlayerController && collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Border")
        {
            onBulletDestroyed?.Invoke(playersBullet);
            Destroy(gameObject);            
        }        
    }
}
