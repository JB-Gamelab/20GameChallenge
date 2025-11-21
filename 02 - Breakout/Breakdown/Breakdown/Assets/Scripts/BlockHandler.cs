using System;
using UnityEngine;

public class BlockHandler : MonoBehaviour
{
    public static event Action<int> onLevelIncrease;
    [SerializeField] private int columns; // Number of columns in grid
    [SerializeField] private int rows; // Number of rows in grid
    [SerializeField] private float spaceX; // X axis spacing
    [SerializeField] private float spaceY; // Y axis pacing
    [SerializeField] private Vector2 startPosition; // First grid position
    [SerializeField] private bool[, ] blockPos; // Booolean array, tells block to spawn in grid position
    [SerializeField] private GameObject blockPrefab;
    private bool notRunning = true;
    private bool running = false;
    private int level = 1;
    private int noBlocks = 0;
    private bool spawnComplete = false;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
        Block.onBlockDestoyed += BlockOnBlockDestroyed;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
        Block.onBlockDestoyed -= BlockOnBlockDestroyed;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Running && notRunning)
        {
            notRunning = false;
            running = true;    
        }
        if (state == GameState.NotRunning)
        {
            notRunning = true;
        }        
    }

    private void BlockOnBlockDestroyed(int obj)
    {
        noBlocks--;
        if (noBlocks < 1)
        {
            level++;
            spawnComplete = false;
            Array.Clear(blockPos, 0, blockPos.Length);         
            onLevelIncrease?.Invoke(level);
        }
    }

    private void Start()
    {
        blockPos = new bool[rows, columns];      
    }

    private void Update()
    {
        if (!spawnComplete && running)
        {
            if (level == 1)
            {
                Level1();
            }
            if (level == 2)
            {
                Level2();
            }
            if (level == 3)
            {
                Level3();
            }
            if (level == 4)
            {
                Level4();
            }
            SpawnBlocks();
            
        }
    }

    private void Level1()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                blockPos[row, col] = true;
                noBlocks++;
            }            
        }
        
    }

    private void Level2()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (col%2 == 0)
                {
                    blockPos[row, col] = true;
                    noBlocks++;
                }                
            }            
        }
    }

    private void Level3()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if ((row%2 == 0 && col%2 != 0) || (row%2 != 0 && col%2 == 0))
                {
                    blockPos[row, col] = true;
                    noBlocks++;
                }
            }            
        }
    }

    private void Level4()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (row%2 == 0)
                {
                    blockPos[row, col] = true;
                    noBlocks++;
                }  
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
        spawnComplete = true;
    }
}
