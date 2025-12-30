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
    [SerializeField] float mediumAsteroidMinSpeed = 0.2f;
    [SerializeField] float mediumAsteroidMaxSpeed = 1.3f;
    [SerializeField] float smallAsteroidMinSpeed = 0.4f;
    [SerializeField] float smallAsteroidMaxSpeed = 1.5f;
    [SerializeField] float spawnTimer = 3.0f;
    [SerializeField] GameObject explosionEffect;

    private List<GameObject> activeLargeAsteroids;

    private void Awake()
    {
        AsteroidController.onAsteroidDestroyed += AsteroidControllerOnAsteroidDestroyed;
    }

    private void OnDestroy()
    {
        AsteroidController.onAsteroidDestroyed -= AsteroidControllerOnAsteroidDestroyed;
    }

    private void AsteroidControllerOnAsteroidDestroyed(int asteroidType, GameObject asteroid, GameObject spawnA, GameObject spawnB)
    {
        if (asteroidType == 1)
        {
            SpawnMediumAsteroid(asteroid, spawnA, spawnB);
            SpawnExplosionEffect(asteroid.transform);        
        }

        if (asteroidType == 2)
        {
            SpawnSmallAsteroid(asteroid, spawnA, spawnB);            
            SpawnExplosionEffect(asteroid.transform);          
        }

        if (asteroidType == 3)
        {
            asteroid.SetActive(false);
            SpawnExplosionEffect(asteroid.transform);          
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
        largeAsteroidRB.angularVelocity = 0;
        largeAsteroid.SetActive(true);
        largeAsteroidSpeed = Random.Range(largeAsteroidMinSpeed, largeAsteroidMaxSpeed);
        largeAsteroidRB.AddForce(largeAsteroid.transform.up * largeAsteroidSpeed, ForceMode2D.Impulse);

        activeLargeAsteroids.Add(largeAsteroid);
        if (activeLargeAsteroids.Count < maxLargeAsteroids)
        {
            StartCoroutine(AsteroidSpawnTimer());
        }
    }

    private void SpawnMediumAsteroid(GameObject currentAsteroid, GameObject spawnPointA, GameObject spawnPointB)
    {
        GameObject mediumAsteroid1;     
        Rigidbody2D mediumAsteroidRB1;
        float mediumAsteroidSpeed1 = UnityEngine.Random.Range(mediumAsteroidMinSpeed, mediumAsteroidMaxSpeed);
        GameObject mediumAsteroid2;     
        Rigidbody2D mediumAsteroidRB2;
        float mediumAsteroidSpeed2 = UnityEngine.Random.Range(mediumAsteroidMinSpeed, mediumAsteroidMaxSpeed);

        mediumAsteroid1 = poolManager.FindFreeMediumAsteroid();        

        if (mediumAsteroid1 == null)
            return;

        mediumAsteroid1.transform.position = spawnPointA.transform.position;
        mediumAsteroidRB1 = mediumAsteroid1.GetComponent<Rigidbody2D>();
        mediumAsteroidRB1.linearVelocity = Vector2.zero;
        mediumAsteroidRB1.angularVelocity = 0;
        mediumAsteroid1.SetActive(true);

        mediumAsteroid2 = poolManager.FindFreeMediumAsteroid();

        if (mediumAsteroid2 == null)
            return;
            
        mediumAsteroid2.transform.position = spawnPointB.transform.position;
        mediumAsteroidRB2 = mediumAsteroid2.GetComponent<Rigidbody2D>();
        mediumAsteroidRB2.linearVelocity = Vector2.zero;
        mediumAsteroidRB2.angularVelocity = 0;
        mediumAsteroid2.SetActive(true);

        activeLargeAsteroids.Remove(currentAsteroid);
        currentAsteroid.SetActive(false);     
        
        mediumAsteroidRB1.AddForce(spawnPointA.transform.up * mediumAsteroidSpeed1, ForceMode2D.Impulse);   
        mediumAsteroidRB2.AddForce(spawnPointB.transform.up * mediumAsteroidSpeed2, ForceMode2D.Impulse);

        if (activeLargeAsteroids.Count < maxLargeAsteroids)
        {
            StartCoroutine(AsteroidSpawnTimer());
        }
    }

    private void SpawnSmallAsteroid(GameObject currentAsteroid, GameObject spawnPointA, GameObject spawnPointB)
    {
        GameObject smallAsteroid1;     
        Rigidbody2D smallAsteroidRB1;
        float smallAsteroidSpeed1 = UnityEngine.Random.Range(smallAsteroidMinSpeed, smallAsteroidMaxSpeed);
        GameObject smallAsteroid2;     
        Rigidbody2D smallAsteroidRB2;
        float smallAsteroidSpeed2 = UnityEngine.Random.Range(smallAsteroidMinSpeed, smallAsteroidMaxSpeed);

        smallAsteroid1 = poolManager.FindFreeSmallAsteroid();        

        if (smallAsteroid1 == null)
            return;

        smallAsteroid1.transform.position = spawnPointA.transform.position;
        smallAsteroidRB1 = smallAsteroid1.GetComponent<Rigidbody2D>();
        smallAsteroidRB1.linearVelocity = Vector2.zero;
        smallAsteroidRB1.angularVelocity = 0;
        smallAsteroid1.SetActive(true);

        smallAsteroid2 = poolManager.FindFreeSmallAsteroid();

        if (smallAsteroid2 == null)
            return;
            
        smallAsteroid2.transform.position = spawnPointB.transform.position;
        smallAsteroidRB2 = smallAsteroid2.GetComponent<Rigidbody2D>();
        smallAsteroidRB2.linearVelocity = Vector2.zero;
        smallAsteroidRB2.angularVelocity = 0;
        smallAsteroid2.SetActive(true);

        activeLargeAsteroids.Remove(currentAsteroid);
        currentAsteroid.SetActive(false);     
        
        smallAsteroidRB1.AddForce(spawnPointA.transform.up * smallAsteroidSpeed1, ForceMode2D.Impulse);   
        smallAsteroidRB2.AddForce(spawnPointB.transform.up * smallAsteroidSpeed2, ForceMode2D.Impulse);
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

    private void SpawnExplosionEffect(Transform position)
    {
        Instantiate(explosionEffect, position.position, Quaternion.identity);
    }

    private IEnumerator AsteroidSpawnTimer()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnLargeAsteroid();
    }
}
