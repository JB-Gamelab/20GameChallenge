using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public static event Action<GameObject> onAsteroidDestroyed;
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
        onAsteroidDestroyed?.Invoke(gameObject);
    }
}
