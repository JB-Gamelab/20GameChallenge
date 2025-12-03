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
}
