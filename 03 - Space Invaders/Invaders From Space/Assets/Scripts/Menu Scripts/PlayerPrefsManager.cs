using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{
    public static void SetVolumes(float musicVol, float sFXVol)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVol);
        PlayerPrefs.SetFloat("SFXVolume", sFXVol);
    }

    public static float GetMusicVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            return musicVolume;
        }
        else
        {
            return 0;
        }
    }

    public static float GetSFXVolume()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            return sFXVolume;
        }
        else
        {
            return 0;
        }
    }

    public static void SetLastGameScore(int score)
    {
        PlayerPrefs.SetInt("LastScore", score);
    }

    public static int GetLastGameScore()
    {
        if (PlayerPrefs.HasKey("LastScore"))
        {
            int score = PlayerPrefs.GetInt("LastScore");
            return score;
        }
        else
        {
            return 0;
        }
    }

    public static void SetHighScores(List<ScoreCard> scoreCards)
    {
        for (int i = 0; i < scoreCards.Count; i++)
        {
            PlayerPrefs.SetString("BestInitial" + i, scoreCards[i].initials);
            PlayerPrefs.SetInt("BestScore" + i, scoreCards[i].score);
        }
    }

    public static List<ScoreCard> GetHighScores()
    {
        List<ScoreCard> highScores = new List<ScoreCard>();
        

        for (int i = 0; i < 5; i++)
        {
            ScoreCard scoreCard = new ScoreCard();
            if (PlayerPrefs.HasKey("BestInitial" + i))
            {
                scoreCard.initials = PlayerPrefs.GetString("BestInitial" + i);
                scoreCard.score = PlayerPrefs.GetInt("BestScore" + i);
            }
            else
            {
                scoreCard.initials = "AAA";
                scoreCard.score = 0;
            }
            highScores.Add(scoreCard);
        }
        return highScores;
    }
}
