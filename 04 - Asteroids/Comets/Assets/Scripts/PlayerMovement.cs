using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float thrust = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private WrapController wrapController;
    
    private Rigidbody2D rB2D;

    private bool leftScreen;

    private void Awake()
    {
        rB2D = GetComponent<Rigidbody2D>();
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
