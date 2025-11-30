using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemyPositions : MonoBehaviour
{
    [SerializeField] private GameObject enemyGroup;

    public Dictionary<int, Transform> lowestPerColumn = new Dictionary<int, Transform>(); // Enemy in lowest position of each column aka firing position

    public List<int> enemyCanShootList;
    public int dictionaryLength;

    public float leftMostX = 3;
    public float rightMostX = -3;

    
    public void CalculateEnemyPositions(List<Transform> enemyTransform) //Figure out the left and right most enemy sprites for movement
    {
        leftMostX = 3;
        rightMostX = -3;
        
        for (int i = 0; i < enemyTransform.Count; i++)
        {
            if (!enemyTransform[i].gameObject.activeSelf) continue;

            Transform enemy = enemyTransform[i].transform;
            
            float x = enemy.localPosition.x;

            if (x < leftMostX)
            {
                leftMostX = x;
            }
            if (x > rightMostX)
            {
                rightMostX = x;
            }

            int columnKey = Mathf.RoundToInt(enemy.position.x);
            if (!lowestPerColumn.ContainsKey(columnKey))
            {
                lowestPerColumn[columnKey] = enemy;
            }
            else
            {
                if (enemy.position.y < lowestPerColumn[columnKey].position.y)
                {
                    lowestPerColumn[columnKey] = enemy;
                }
            }            
        }

        enemyCanShootList = new List<int>(lowestPerColumn.Keys); //Adds dictionary entries to a list  
    }
}
