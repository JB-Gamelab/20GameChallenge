using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxXPos = 8f;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameManager gameManager;
    private Vector2 move;

    private bool bulletExists = false;

    private void Awake()
    {
        BulletMovement.onBulletDestroyed += BulletMovementOnBulletDestroyed;
    }

    private void BulletMovementOnBulletDestroyed(bool obj)
    {
        bulletExists = false;
    }

    private void OnMove(InputValue val)
    {
        move = val.Get<Vector2>();
    }

    private void OnFire()
    {
        //bulletManager.PlayerFire(bulletSpawnPoint.transform);
        if (!bulletExists)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
            BulletMovement bulletInstance = bullet.GetComponent<BulletMovement>();
            bulletInstance.owner = this;
            bulletExists = true;
        }
    }

    private void Update()
    {
        if (transform.position.x >= maxXPos * -1 && transform.position.x <= maxXPos)
        {
            transform.position += speed * Time.deltaTime * new Vector3(move.x, 0f, 0f);
        }
        if (transform.position.x < maxXPos * -1)
        {
            transform.position = new Vector2(maxXPos * -1, transform.position.y);
        }
        if (transform.position.x > maxXPos)
        {
            transform.position = new Vector2(maxXPos, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            gameManager.UpdateGameState(GameManager.GameState.LifeLost);
        }
    }
}
