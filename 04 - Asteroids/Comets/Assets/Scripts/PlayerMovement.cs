using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static event Action onPlayerThrust;

    [SerializeField] private float thrust = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private WrapController wrapController;
    [SerializeField] private SpriteRenderer thrustSprite;
    [SerializeField] private SpriteRenderer leftSideThrust;
    [SerializeField] private SpriteRenderer rightSideThrust;
    
    private Rigidbody2D rB2D;

    private bool leftScreen;
    private bool pauseState = false;

    private void Awake()
    {
        rB2D = GetComponent<Rigidbody2D>();
        thrustSprite.gameObject.SetActive(false);
        leftSideThrust.gameObject.SetActive(false);
        rightSideThrust.gameObject.SetActive(false);
        GameManager.onPause += GameManagerOnPause;
    }

    private void OnDestroy()
    {
        GameManager.onPause -= GameManagerOnPause;
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

    public void OnThrust(InputAction.CallbackContext context)
    {
        if(!pauseState)
        {
            if (context.performed)
            {
                rB2D.AddForce(transform.up * thrust);
                thrustSprite.gameObject.SetActive(true);
            }
            if (context.canceled)
            {
                thrustSprite.gameObject.SetActive(false);
            }
            onPlayerThrust?.Invoke();
        }
    }

    public void OnRotateAntiClockwise(InputAction.CallbackContext context)
    {
        if(!pauseState)
        {
            if (context.performed)
            {
                rB2D.angularVelocity += rotateSpeed;
                rightSideThrust.gameObject.SetActive(true);
            }
            if (context.canceled)
            {
                rightSideThrust.gameObject.SetActive(false);
            }
        }
    }

    public void OnRotateClockwise(InputAction.CallbackContext context)
    {
        if (!pauseState)
        {
            if (context.performed)
            {
                rB2D.angularVelocity -= rotateSpeed;
                leftSideThrust.gameObject.SetActive(true);
            }
            if (context.canceled)
            {
                leftSideThrust.gameObject.SetActive(false);
            }
        }
    }

    private void GameManagerOnPause(bool isPaused)
    {
        pauseState = isPaused;
    }
}
