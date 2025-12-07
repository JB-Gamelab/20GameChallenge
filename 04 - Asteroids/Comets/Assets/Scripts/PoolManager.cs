using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private int numberOfBullets = 20;
    [SerializeField] private GameObject bulletPrefab;

    private List<GameObject> bulletList;
    private GameObject freeBullet;

    private void Start()
    {
        bulletList = new List<GameObject>();

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, transform);
            bulletInstance.SetActive(false);
            bulletList.Add(bulletInstance);
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
}
