using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    private void Awake()
    {
        EnemyController.onEnemyScored += EnemyControllerOnEnemyScored;
    }

    private void OnDestroy()
    {
        EnemyController.onEnemyScored -= EnemyControllerOnEnemyScored;
    }

    private void EnemyControllerOnEnemyScored(int value)
    {
        score = score + value;
    }
}
