using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject wrapControllerGO;
    private WrapController wrapController;
    private Rigidbody2D rb2D;
    [SerializeField] private float lifetime = 5;
    private bool leftScreen;

    
    private void Awake()
    {
        wrapControllerGO = GameObject.Find("WrapController");
        wrapController = wrapControllerGO.GetComponent<WrapController>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(BulletLifetime());
    }


    private void Update()
    {
        leftScreen = wrapController.CheckPosition(transform.position);
        BulletLifetime();
    }

    private void LateUpdate()
    {
        if (leftScreen)
        {
            transform.position = wrapController.WarpPosition(transform.position);
        }
    }

    private IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        BulletDestroy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            BulletDestroy();
        }
    }

    private void BulletDestroy()
    {
        rb2D.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
