using System;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private float moveSpeed = 3;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.up;

        if (transform.position.y >= startPosition.y + 1)
        {
            Destroy(gameObject);
        }
    }
}
