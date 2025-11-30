using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static event Action<int> onEnemyCountChanged;

    [SerializeField] private EnemyPositions enemyPositions;

    public List<Transform> enemyList;

    private void Awake()
    {
        EnemyController.onEnemyDestroyed += EnemyControllerOnEnemyDestroyed;
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform enemy = transform.GetChild(i);
            enemyList.Add(enemy);
        }

        enemyPositions.CalculateEnemyPositions(enemyList);
        
        onEnemyCountChanged?.Invoke(enemyList.Count);
    } 
    
    private void EnemyControllerOnEnemyDestroyed(Transform enemyTransform)
    {
        enemyList.Remove(enemyTransform);

        enemyPositions.lowestPerColumn.Clear();
        enemyPositions.CalculateEnemyPositions(enemyList);
        onEnemyCountChanged?.Invoke(enemyList.Count);
    }
}
