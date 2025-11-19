using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float spawnRate = 10.0f;

    private float elapsedTime = 0.0f;
    private float spawnHeight;

    private bool running;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Running)
        {
            running = true;
        } else
        {
            running = false;
        }
    }

    private void Update()
    {
        if (running)
        {
            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
            }
            else
            {
                spawnHeight = Random.Range(-3.5f, 2.0f);
                Vector3 spawnVector = new Vector3(12.0f, spawnHeight, 0);
                Instantiate(obstacle, spawnVector, Quaternion.identity);
                elapsedTime = spawnRate;
            }
        }
    }
}
