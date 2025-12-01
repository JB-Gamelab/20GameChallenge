using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static event Action<int> onEnemyCountChanged;

    [SerializeField] private EnemyPositions enemyPositions;
    [SerializeField] private GameManager gameManager;

    public List<Transform> enemyList;

    private int enemyCount;

    private void Awake()
    {
        EnemyController.onEnemyDestroyed += EnemyControllerOnEnemyDestroyed;
    }

    private void Start()
    {
        enemyCount = transform.childCount;
        BuildEnemyList();

        enemyPositions.CalculateEnemyPositions(enemyList);
        
        onEnemyCountChanged?.Invoke(enemyList.Count);
    }

    private void Update()
    {
        if (enemyCount == 0)
        {
            gameManager.UpdateGameState(GameManager.GameState.LevelLoad);
            BuildEnemyList();
            enemyPositions.CalculateEnemyPositions(enemyList);
            onEnemyCountChanged?.Invoke(enemyList.Count);
            enemyCount = transform.childCount;
        }
    }

    private void EnemyControllerOnEnemyDestroyed(Transform enemyTransform)
    {
        enemyList.Remove(enemyTransform);

        enemyPositions.lowestPerColumn.Clear();
        enemyPositions.CalculateEnemyPositions(enemyList);
        onEnemyCountChanged?.Invoke(enemyList.Count);
        enemyCount--;
    }

    private void BuildEnemyList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform enemy = transform.GetChild(i);
            enemyList.Add(enemy);
        }
    }
}
