using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxXPos = 8f;
    [SerializeField] private float yPos = -3.6f;
    private Vector2 move;

    private void OnMove(InputValue val)
    {
        move = val.Get<Vector2>();
    }

    private void Update()
    {
        if (transform.position.x >= maxXPos * -1 && transform.position.x <= maxXPos)
        {
            transform.position += speed * Time.deltaTime * new Vector3(move.x, 0f, 0f);
        }
        if (transform.position.x < maxXPos * -1)
        {
            transform.position = new Vector2(maxXPos * -1, yPos);
        }
        if (transform.position.x > maxXPos)
        {
            transform.position = new Vector2(maxXPos, yPos);
        }
    }
}
