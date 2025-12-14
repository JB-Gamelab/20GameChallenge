using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> onScoreChanged;
    private int score;

    private void Awake()
    {
        AsteroidController.onAsteroidDestroyed += AsteroidControllerOnAsteroidDestroyed;
        score = 0;
    }

    private void OnDestroy()
    {
        AsteroidController.onAsteroidDestroyed -= AsteroidControllerOnAsteroidDestroyed;
    }

    private void AsteroidControllerOnAsteroidDestroyed(int arg1, GameObject object1, GameObject object2, GameObject object3)
    {
        if (arg1 == 1)
        {
            score = score + 100;
        }
        if (arg1 == 2)
        {
            score = score + 500;
        }
        if (arg1 == 3)
        {
            score = score + 1000;
        }
        onScoreChanged?.Invoke(score);
    }
}
