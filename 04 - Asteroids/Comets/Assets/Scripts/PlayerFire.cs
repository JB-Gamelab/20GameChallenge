using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] PoolManager poolManager;

    [SerializeField] private GameObject bulletSpawn;

    [SerializeField] private float bulletSpeed = 2f;

    private Rigidbody2D playerRB;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject bullet;     
            Rigidbody2D bulletRB;

            bullet = poolManager.FindFreeBullet();

            if (bullet == null)
                return;
            
            bullet.transform.position = bulletSpawn.transform.position;
            bullet.transform.rotation = bulletSpawn.transform.rotation;
            bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.linearVelocity = Vector2.zero;
            bullet.SetActive(true);
            bulletRB.AddForce(bulletSpawn.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
