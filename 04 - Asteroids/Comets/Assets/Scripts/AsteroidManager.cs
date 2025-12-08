using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] PoolManager poolManager;
    [SerializeField] int maxLargeAsteroids = 5;
    [SerializeField] float largeAsteroidMinSpeed = 0.1f;
    [SerializeField] float largeAsteroidMaxSpeed = 1.0f;
    [SerializeField] float spawnTimer = 3.0f;

    private List<GameObject> activeLargeAsteroids;

    private void Awake()
    {
        AsteroidController.onAsteroidDestroyed += AsteroidControllerOnAsteroidDestroyed;
    }

    private void AsteroidControllerOnAsteroidDestroyed(GameObject @object)
    {
        activeLargeAsteroids.Remove(@object);
        @object.SetActive(false);
        if (activeLargeAsteroids.Count < maxLargeAsteroids)
        {
            StartCoroutine(AsteroidSpawnTimer());
        }
    }

    private void Start()
    {
        activeLargeAsteroids = new List<GameObject>();

        if (activeLargeAsteroids.Count < maxLargeAsteroids)
        {
            StartCoroutine(AsteroidSpawnTimer());
        }
    }

    private void SpawnLargeAsteroid()
    {
        GameObject largeAsteroid;     
        Rigidbody2D largeAsteroidRB;
        float largeAsteroidSpeed;

        largeAsteroid = poolManager.FindFreeLargeAsteroid();

        if (largeAsteroid == null)
            return;
            
        float rotationAngle = GetRotationAngle();        
        largeAsteroid.transform.Rotate(0.0f, 0.0f, rotationAngle);

        largeAsteroid.transform.position = GetBestSpawnLocation(rotationAngle);
     
        largeAsteroidRB = largeAsteroid.GetComponent<Rigidbody2D>();
        largeAsteroidRB.linearVelocity = Vector2.zero;
        largeAsteroid.SetActive(true);
        largeAsteroidSpeed = Random.Range(largeAsteroidMinSpeed, largeAsteroidMaxSpeed);
        largeAsteroidRB.AddForce(largeAsteroid.transform.up * largeAsteroidSpeed, ForceMode2D.Impulse);

        activeLargeAsteroids.Add(largeAsteroid);
        if (activeLargeAsteroids.Count < maxLargeAsteroids)
        {
            StartCoroutine(AsteroidSpawnTimer());
        }
    }

    private float GetRotationAngle()
    {
        return Random.Range(0.0f, 360.0f); 
    }

    private Vector2 GetBestSpawnLocation(float rotationAngle)
    {
        Vector2 spawnLoaction = Vector2.zero;

        if (rotationAngle > 315 || rotationAngle <= 45)
        {
            spawnLoaction = new Vector2(Random.Range(-5.0f, 5.0f), -6.0f); // Bottom of screen
        }
        if (rotationAngle > 45 || rotationAngle <= 135)
        {
            spawnLoaction = new Vector2(6.0f, Random.Range(-5.0f, 5.0f)); // Right of screen
        }
        if (rotationAngle > 135 || rotationAngle <= 225)
        {
            spawnLoaction = new Vector2(Random.Range(-5.0f, 5.0f), 6.0f); // Top of screen
        }
        if (rotationAngle > 225 || rotationAngle <= 315)
        {
            spawnLoaction = new Vector2(-6.0f, Random.Range(-5.0f, 5.0f)); // Left of screen
        }

        return spawnLoaction;
    }

    private IEnumerator AsteroidSpawnTimer()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnLargeAsteroid();
    }
}
