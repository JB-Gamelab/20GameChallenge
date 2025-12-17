using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action onPlayerDeath;

    [SerializeField] private float invulTimer = 4;
    [SerializeField] private float flashTimer = 0.5f;
    [SerializeField] private GameObject playerSprite; 
    [SerializeField] private GameObject shieldSprite;
    [SerializeField] private int shieldTimer = 4;

    private Rigidbody2D rB2D;
    private CapsuleCollider2D capCollider2D;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        rB2D = GetComponent<Rigidbody2D>();
        capCollider2D = GetComponent<CapsuleCollider2D>();
        spriteRend = playerSprite.GetComponent<SpriteRenderer>();
        GameManager.onRespawn += GameManagerOnRespawn;
        PowerUpController.onPowerUp += PowerUpControllerOnPowerUp;
    }

    private void OnDestroy()
    {
        GameManager.onRespawn -= GameManagerOnRespawn;
        PowerUpController.onPowerUp -= PowerUpControllerOnPowerUp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            onPlayerDeath?.Invoke();
            rB2D.linearVelocity = Vector2.zero;
            rB2D.angularVelocity = 0;
            gameObject.SetActive(false);
        }
    }

    private void GameManagerOnRespawn()
    {
        StartCoroutine(RespawnInvulTimer());
    }

    private IEnumerator RespawnInvulTimer()
    {
        capCollider2D.enabled = false;
        float timer = 0;

        while (timer < invulTimer)
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(flashTimer);
            if (spriteRend.enabled)
            {
                spriteRend.enabled = false;
            }
            else
            {
                spriteRend.enabled = true;
            }
        }
        
        spriteRend.enabled = true;
        capCollider2D.enabled = true;
    }

    private void PowerUpControllerOnPowerUp(int powerUpType)
    {
        if (powerUpType == 2)
        {
            StartCoroutine(ShieldTimer());
        }
    }

    private IEnumerator ShieldTimer()
    {
        capCollider2D.enabled = false;
        shieldSprite.SetActive(true);
        yield return new WaitForSeconds(shieldTimer);
        capCollider2D.enabled = true;
        shieldSprite.SetActive(false);
    }
}
