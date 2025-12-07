using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] PoolManager poolManager;

    [SerializeField] private GameObject bulletSpawn;

    private float bulletSpeed = 20f;

    private void OnFire()
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
        bulletRB.AddForce(bulletSpawn.transform.up * bulletSpeed);
    }
}
