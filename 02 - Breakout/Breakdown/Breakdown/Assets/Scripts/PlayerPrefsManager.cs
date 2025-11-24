using UnityEngine;

public static class PlayerPrefsManager
{
    private static readonly string ScoreKey = "PlayerScore";

    public static void SaveScore(int score)
    {
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.Save();
    }

    public static int LoadScore()
    {
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            int score = PlayerPrefs.GetInt(ScoreKey);
            return score;
        }
        else
        {
            return 0;
        }
    }
}
