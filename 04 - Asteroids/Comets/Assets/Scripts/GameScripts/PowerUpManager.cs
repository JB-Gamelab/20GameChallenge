using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private Sprite extraLifeSprite;
    [SerializeField] private Sprite shieldSprite;
    [SerializeField] private Sprite triShotSprite;
    [SerializeField] private int randomChance = 80;

    private void Awake()
    {
        AsteroidController.onAsteroidDestroyed += AsteroidControllerOnAsteroidDestroyed;
    }

    private void OnDestroy()
    {
        AsteroidController.onAsteroidDestroyed -= AsteroidControllerOnAsteroidDestroyed;
    }

    private void AsteroidControllerOnAsteroidDestroyed(int arg1, GameObject object1, GameObject object2, GameObject object3)
    {
        if (arg1 == 3)
        {
            int randomSpawn = Random.Range(1, 101);
            if (randomSpawn > randomChance)
            {
                int randomPowerUp = Random.Range(1, 4);
                SpawnRandomPowerUp(object1, randomPowerUp);
            }
        }
    }

    private void SpawnRandomPowerUp(GameObject asteroid, int randomPowerUp)
    {
        GameObject destroyedAsteroid = asteroid;
        int powerUpType = randomPowerUp;
        GameObject powerUp = poolManager.FindFreePowerUp();

        if (powerUp != null)
        {
            SpriteRenderer powerUpSprite = powerUp.GetComponentInChildren<SpriteRenderer>();

            powerUp.transform.position = destroyedAsteroid.transform.position;

            if (powerUpType == 1) // Extra Life
            {
                powerUpSprite.sprite = extraLifeSprite;
            }
            else if (powerUpType == 2) // Shield
            {
                powerUpSprite.sprite = shieldSprite;
            }
            else if (powerUpType == 3) // Tri Shot
            {
                powerUpSprite.sprite = triShotSprite;
            }

            powerUp.SetActive(true);
        }
    }
}
