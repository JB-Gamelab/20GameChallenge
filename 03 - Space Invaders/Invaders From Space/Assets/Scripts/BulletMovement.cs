using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private bool up;

    private void Start()
    {
        if (transform.position.y <= -3f)
        {
            up = true;
        }
        else
        {
            up = false;
        }
    }

    private void Update()
    {
        if (up)
        {
            transform.position += Time.deltaTime * new Vector3(0, speed, 0);
        }
        else
        {
            transform.position -= Time.deltaTime * new Vector3(0, speed, 0);
        }
    }

}
