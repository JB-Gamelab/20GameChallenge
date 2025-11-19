using System.Data;
using UnityEngine;

public class BlockHandler : MonoBehaviour
{
    [SerializeField] private int columns; // Number of columns in grid
    [SerializeField] private int rows; // Number of rows in grid
    [SerializeField] private float spaceX; // X axis spacing
    [SerializeField] private float spaceY; // Y axis pacing
    [SerializeField] private Vector2 startPosition; // First grid position
    [SerializeField] private bool[, ] blockPos; // Booolean array, tells block to spawn in grid position
    [SerializeField] private GameObject blockPrefab;

    private bool running;
    private bool notRunning;
    private bool lifeLost;

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
        if (state == GameState.Running && notRunning)
        {
            SpawnBlocks();
            notRunning = false;          
        }
        if (state == GameState.NotRunning)
        {
            notRunning = true;
        }        
    }

        private void Start()
    {
        blockPos = new bool[rows, columns];
        Level1();        
    }

    private void Level1()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                blockPos[row, col] = true;
            }            
        }
    }

    private void SpawnBlocks()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 spawnPos = new Vector2(startPosition.x + col * spaceX, startPosition.y + row * spaceY);
                if (blockPos[row, col])
                {
                    Instantiate(blockPrefab, spawnPos, Quaternion.identity);
                }
            }            
        }
    }
}
