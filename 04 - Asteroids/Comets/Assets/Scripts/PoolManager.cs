using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private int numberOfBullets = 30;
    [SerializeField] private int numberOfLargeAsteroids = 5;
    [SerializeField] private int numberOfMediumAsteroids = 10;
    [SerializeField] private int numberOfSmallAsteroids = 20;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject largeAsteroidPrefab;
    [SerializeField] private GameObject mediumAsteroidPrefab;
    [SerializeField] private GameObject smallAsteroidPrefab;

    private List<GameObject> bulletList;
    private List<GameObject> largeAsteroidList;
    private List<GameObject> mediumAsteroidList;
    private List<GameObject> smallAsteroidList;
    private GameObject freeBullet;
    private GameObject freeLargeAsteroid;
    private GameObject freeMediumAsteroid;
    private GameObject freeSmallAsteroid;

    private void Start()
    {
        bulletList = new List<GameObject>();
        largeAsteroidList = new List<GameObject>();
        mediumAsteroidList = new List<GameObject>();
        smallAsteroidList = new List<GameObject>();

        CreatePool(bulletList, numberOfBullets, bulletPrefab);
        CreatePool(largeAsteroidList, numberOfLargeAsteroids, largeAsteroidPrefab);
        CreatePool(mediumAsteroidList, numberOfMediumAsteroids, mediumAsteroidPrefab);
        CreatePool(smallAsteroidList, numberOfSmallAsteroids, smallAsteroidPrefab);
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
}
