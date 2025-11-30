using Unity.VisualScripting;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private EnemyPositions enemyPositions;
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private GameObject bulletPrefab;
    private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 20f;

    private float fireDelay = 0;

    private void Update()
    {
        fireDelay = fireDelay + Time.deltaTime;
        if (fireDelay >= fireRate)
        {
            if (enemyPositions.enemyCanShootList.Count > 0)
            {
                int randomEnemy = Random.Range(0, enemyPositions.enemyCanShootList.Count); //pick random number from total number of enemies in list
                int listRandom = enemyPositions.enemyCanShootList[randomEnemy]; //grab the index of the random number
                Transform enemy = enemyPositions.lowestPerColumn[listRandom];
                //bulletManager.EnemyFire(bulletSpawnPoint);
                if (enemy != null)
                {
                    bulletSpawnPoint = enemy.GetChild(1); //pull the data from the index in the dictionary
                    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
                    BulletMovement bulletInstance = bullet.GetComponent<BulletMovement>();
                    bulletInstance.owner = this;            
                }
                fireDelay = 0;
            }
        }
    }
}
