using System;
using System.Collections;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public static event Action<int> onPowerUp;
    private SpriteRenderer spriteRendererActive;
    private Sprite activeSprite;
    private int powerUpType;

    private void Start()
    {
        spriteRendererActive = GetComponentInChildren<SpriteRenderer>();
        activeSprite = spriteRendererActive.sprite;

        if (activeSprite.name == "1UpPowerUp_0")
        {
            powerUpType = 1;
        }
        if (activeSprite.name == "ShieldPowerUp_0")
        {
            powerUpType = 2;
        }
        if (activeSprite.name == "TriShotPowerUp_0")
        {
            powerUpType = 3;
        }
        StartCoroutine(powerUpLife());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onPowerUp?.Invoke(powerUpType);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator powerUpLife()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
