using Unity.VisualScripting;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private EnemyPositions enemyPositions;
    [SerializeField] private BulletManager bulletManager;
    private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 20f;

    private float fireDelay = 0;

    private void Update()
    {
        fireDelay = fireDelay + Time.deltaTime;
        if (fireDelay >= fireRate)
        {
            int randomEnemy = Random.Range(0, enemyPositions.enemyCanShootList.Count); //pick random number from total number of enemies in list
            int listRandom = enemyPositions.enemyCanShootList[randomEnemy]; //grab the index of the random number
            bulletSpawnPoint = enemyPositions.lowestPerColumn[listRandom].GetChild(1); //pull the data from the index in the dictionary
            bulletManager.EnemyFire(bulletSpawnPoint);
            fireDelay = 0;
        }
    }
}
