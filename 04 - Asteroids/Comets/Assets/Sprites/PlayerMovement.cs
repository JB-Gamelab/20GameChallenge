using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float thrust = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    
    private Rigidbody2D rB2D;

    private void Awake()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }
    private void OnThrust()
    {
        rB2D.AddForce(transform.up * thrust);
    }

    private void OnRotateAntiClockwise()
    {
        rB2D.angularVelocity += rotateSpeed;
    }

    private void OnRotateClockwise()
    {
        rB2D.angularVelocity -= rotateSpeed;
    }
}
