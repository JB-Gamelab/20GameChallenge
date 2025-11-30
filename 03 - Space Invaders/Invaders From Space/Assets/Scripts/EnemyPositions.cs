using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositions : MonoBehaviour
{
    [SerializeField] private GameObject enemyGroup;

    public Dictionary<int, Transform> lowestPerColumn = new Dictionary<int, Transform>(); // Enemy in lowest position of each column aka firing position

    public List<int> enemyCanShootList;
    public int dictionaryLength;

    public float[] CalculateEnemyPositions(float leftMostX, float rightMostX) //Figure out the left and right most enemy sprites for movement
    {
        float[] leftRightX = new float[2];

        for (int i = 0; i < enemyGroup.transform.childCount; i++)
        {
            Transform enemy = enemyGroup.transform.GetChild(i);

            if (!enemy || !enemy.gameObject) continue;
            
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
        
        leftRightX[0] = leftMostX;
        leftRightX[1] = rightMostX;
        
        return leftRightX;
    }
}
