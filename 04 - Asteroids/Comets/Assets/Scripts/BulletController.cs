using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject wrapControllerGO;
    private WrapController wrapController;
    private Rigidbody2D rb2D;
    [SerializeField] private float lifetime = 5;
    private float startTime = 0;
    
    private void Awake()
    {
        wrapControllerGO = GameObject.Find("WrapController");
        wrapController = wrapControllerGO.GetComponent<WrapController>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private bool leftScreen;

    private void Update()
    {
        leftScreen = wrapController.CheckPosition(transform.position);

        if (startTime > lifetime)
        {
            BulletDestroy();
        }
        else
        {
            startTime = startTime + Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (leftScreen)
        {
            transform.position = wrapController.WarpPosition(transform.position);
        }
    }

    private void BulletDestroy()
    {
        rb2D.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
