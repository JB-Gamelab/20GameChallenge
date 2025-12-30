using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public static event Action<int, GameObject, GameObject, GameObject> onAsteroidDestroyed;

    [SerializeField] private AsteroidType asteroidType;
    [SerializeField] private GameObject spawnPointA;
    [SerializeField] private GameObject spawnPointB;

    private GameObject wrapControllerGO;
    private WrapController wrapController;

    private bool leftScreen;

    private void Awake()
    {
        wrapControllerGO = GameObject.Find("WrapController");
        wrapController = wrapControllerGO.GetComponent<WrapController>();
    }

    private void Update()
    {
        leftScreen = wrapController.CheckPosition(transform.position);
    }

    private void LateUpdate()
    {
        if (leftScreen)
        {
            transform.position = wrapController.WarpPosition(transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            AsteroidDestroy();
            
        }
    }

    private void AsteroidDestroy()
    {
        if (asteroidType == AsteroidType.Large)
        {
            int asteroidNum = 1;
            onAsteroidDestroyed?.Invoke(asteroidNum, gameObject, spawnPointA, spawnPointB);
        }
        
        if (asteroidType == AsteroidType.Medium)
        {
            int asteroidNum = 2;
            onAsteroidDestroyed?.Invoke(asteroidNum, gameObject, spawnPointA, spawnPointB);
        }

        if (asteroidType == AsteroidType.Small)
        {
            int asteroidNum = 3;
            onAsteroidDestroyed?.Invoke(asteroidNum, gameObject, spawnPointA, spawnPointB);
        }
    }

    public enum AsteroidType
    {
        Large,
        Medium,
        Small
    }
}
