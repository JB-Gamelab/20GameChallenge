using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector2 move;

    private void OnMove(InputValue val)
    {
        move = val.Get<Vector2>();
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * new Vector3(move.x, -3.6f, 0f);
    }
}
