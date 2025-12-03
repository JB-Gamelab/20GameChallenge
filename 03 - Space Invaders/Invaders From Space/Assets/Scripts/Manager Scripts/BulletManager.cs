using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    public void PlayerFire(Transform transform)
    {
        SpriteRenderer bulletSprite = bulletPrefab.GetComponent<SpriteRenderer>();
        bulletSprite.color = Color.greenYellow;

        Vector3 transformVector3 = new Vector3(transform.position.x, transform.position.y, 0);

        Instantiate(bulletPrefab, transformVector3, Quaternion.identity);
    }

    public void EnemyFire(Transform transform)
    {
        SpriteRenderer bulletSprite = bulletPrefab.GetComponent<SpriteRenderer>();
        bulletSprite.color = Color.orange;

        Vector3 transformVector3 = new Vector3(transform.position.x, transform.position.y, 0);

        Instantiate(bulletPrefab, transformVector3, Quaternion.identity);
    }
}
