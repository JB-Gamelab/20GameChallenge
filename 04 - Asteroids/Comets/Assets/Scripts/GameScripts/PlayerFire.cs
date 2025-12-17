using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public static event Action onPlayerFire;

    [SerializeField] PoolManager poolManager;

    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private GameObject triShotSpawnLeft;
    [SerializeField] private GameObject triShotSpawnRight;
    [SerializeField] private int triShotTimer = 5;

    [SerializeField] private float bulletSpeed = 2f;

    private bool pauseState = false;
    private bool triShotActive = false;

    private void Awake()
    {
        GameManager.onPause += GameManagerOnPause;
        PowerUpController.onPowerUp += PowerUpControllerOnPowerUp;
    }

    private void OnDestroy()
    {
        GameManager.onPause -= GameManagerOnPause;
        PowerUpController.onPowerUp -= PowerUpControllerOnPowerUp;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!pauseState)
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

                if (triShotActive)
                {
                    GameObject bullet1;     
                    Rigidbody2D bullet1RB;

                    bullet1 = poolManager.FindFreeBullet();

                    if (bullet1 == null)
                        return;
                
                    bullet1.transform.position = triShotSpawnLeft.transform.position;
                    bullet1.transform.rotation = triShotSpawnLeft.transform.rotation;
                    bullet1RB = bullet1.GetComponent<Rigidbody2D>();
                    bullet1RB.linearVelocity = Vector2.zero;
                    bullet1.SetActive(true);
                    bullet1RB.AddForce(triShotSpawnLeft.transform.up * bulletSpeed, ForceMode2D.Impulse);

                    GameObject bullet2;     
                    Rigidbody2D bullet2RB;

                    bullet2 = poolManager.FindFreeBullet();

                    if (bullet2 == null)
                        return;
                
                    bullet2.transform.position = triShotSpawnRight.transform.position;
                    bullet2.transform.rotation = triShotSpawnRight.transform.rotation;
                    bullet2RB = bullet2.GetComponent<Rigidbody2D>();
                    bullet2RB.linearVelocity = Vector2.zero;
                    bullet2.SetActive(true);
                    bullet2RB.AddForce(triShotSpawnRight.transform.up * bulletSpeed, ForceMode2D.Impulse);
                }
            }
            onPlayerFire?.Invoke();
        }
    }

    private void GameManagerOnPause(bool isPaused)
    {
        pauseState = isPaused;
    }

    private void PowerUpControllerOnPowerUp(int powerUpType)
    {
        if (powerUpType == 3)
        {
            StartCoroutine(TriShotTimer());
        }
    }

    private IEnumerator TriShotTimer()
    {
        triShotActive = true;
        yield return new WaitForSeconds(triShotTimer);
        triShotActive = false;
    }
}
