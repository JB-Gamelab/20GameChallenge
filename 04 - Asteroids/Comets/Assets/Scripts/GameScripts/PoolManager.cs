using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private int numberOfBullets = 30;
    [SerializeField] private int numberOfLargeAsteroids = 5;
    [SerializeField] private int numberOfMediumAsteroids = 10;
    [SerializeField] private int numberOfSmallAsteroids = 20;
    [SerializeField] private int numberofPowerUps = 5;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject largeAsteroidPrefab;
    [SerializeField] private GameObject mediumAsteroidPrefab;
    [SerializeField] private GameObject smallAsteroidPrefab;
    [SerializeField] private GameObject powerUpPrefab;

    private List<GameObject> bulletList;
    private List<GameObject> largeAsteroidList;
    private List<GameObject> mediumAsteroidList;
    private List<GameObject> smallAsteroidList;
    private List<GameObject> powerUpList;

    private GameObject freeBullet;
    private GameObject freeLargeAsteroid;
    private GameObject freeMediumAsteroid;
    private GameObject freeSmallAsteroid;
    private GameObject freePowerUp;

    private void Awake()
    {
        PlayerController.onPlayerDeath += PlayerControllerOnPlayerDeath;
        GameManager.onPause += GameManagerOnPause;
    }

    private void OnDestroy()
    {
        PlayerController.onPlayerDeath -= PlayerControllerOnPlayerDeath;
        GameManager.onPause -= GameManagerOnPause;
    }

    private void Start()
    {
        bulletList = new List<GameObject>();
        largeAsteroidList = new List<GameObject>();
        mediumAsteroidList = new List<GameObject>();
        smallAsteroidList = new List<GameObject>();
        powerUpList = new List<GameObject>();

        CreatePool(bulletList, numberOfBullets, bulletPrefab);
        CreatePool(largeAsteroidList, numberOfLargeAsteroids, largeAsteroidPrefab);
        CreatePool(mediumAsteroidList, numberOfMediumAsteroids, mediumAsteroidPrefab);
        CreatePool(smallAsteroidList, numberOfSmallAsteroids, smallAsteroidPrefab);
        CreatePool(powerUpList, numberofPowerUps, powerUpPrefab);
    }

    private void CreatePool(List<GameObject> objectList, int numObjects, GameObject objectPrefab)
    {
        for (int i = 0; i < numObjects; i++)
        {
            GameObject objectInstance = Instantiate(objectPrefab,transform);
            objectInstance.SetActive(false);
            objectList.Add(objectInstance);
        }
    }

    public GameObject FindFreeBullet()
    {   
        freeBullet = null;

        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                freeBullet = bulletList[i];
                return freeBullet; 
            }
        }       
        return freeBullet;        
    }

    public GameObject FindFreeLargeAsteroid()
    {   
        freeLargeAsteroid = null;

        for (int i = 0; i < largeAsteroidList.Count; i++)
        {
            if (!largeAsteroidList[i].activeSelf)
            {
                freeLargeAsteroid = largeAsteroidList[i];
                return freeLargeAsteroid; 
            }
        }       
        return freeLargeAsteroid;        
    }

    public GameObject FindFreeMediumAsteroid()
    {   
        freeMediumAsteroid = null;

        for (int i = 0; i < mediumAsteroidList.Count; i++)
        {
            if (!mediumAsteroidList[i].activeSelf)
            {
                freeMediumAsteroid = mediumAsteroidList[i];
                return freeMediumAsteroid; 
            }
        }       
        return freeMediumAsteroid;        
    }

    public GameObject FindFreeSmallAsteroid()
    {   
        freeSmallAsteroid = null;

        for (int i = 0; i < smallAsteroidList.Count; i++)
        {
            if (!smallAsteroidList[i].activeSelf)
            {
                freeSmallAsteroid = smallAsteroidList[i];
                return freeSmallAsteroid; 
            }
        }       
        return freeSmallAsteroid;        
    }

    public GameObject FindFreePowerUp()
    {   
        freePowerUp = null;

        for (int i = 0; i < powerUpList.Count; i++)
        {
            if (!powerUpList[i].activeSelf)
            {
                freePowerUp = powerUpList[i];
                return freePowerUp; 
            }
        }       
        return freePowerUp;        
    }

    public void PlayerControllerOnPlayerDeath()
    {
        // Deactivates any active bullets on death
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i].activeSelf)
            {
                bulletList[i].gameObject.SetActive(false);
            }
        }
        // Deactivates active power ups on death
        for (int i = 0; i < powerUpList.Count; i++)
        {
            if (powerUpList[i].activeSelf)
            {
                powerUpList[i].gameObject.SetActive(false);
            }
        } 
    }

    private void GameManagerOnPause(bool isPaused)
    {
        PauseMotion(bulletList, isPaused);
        PauseMotion(largeAsteroidList, isPaused);
        PauseMotion(mediumAsteroidList, isPaused);
        PauseMotion(smallAsteroidList, isPaused);
    }

    private void PauseMotion(List<GameObject> objectList, bool isPaused)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i].activeSelf)
            {
                objectList[i].GetComponent<Rigidbody2D>().simulated = !isPaused;
            }
        }
    }
}
