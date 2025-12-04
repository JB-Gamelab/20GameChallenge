using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public static event Action onClick;

    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sFXVolume;

    public void StartGame()
    {
        onClick?.Invoke();
        PlayerPrefsManager.SetVolumes(musicVolume.value, sFXVolume.value);
        SceneManager.LoadScene(1);
    }

    public void HighScore()
    {
        onClick?.Invoke();
        SceneManager.LoadScene(2);
        PlayerPrefsManager.SetLastGameScore(0);
    }

    public void Credits()
    {
        onClick?.Invoke();
        SceneManager.LoadScene(3);
    }

    public float GetMusicVolume()
    {
        return musicVolume.value;
    }

    public float GetSFXVolume()
    {
        return sFXVolume.value;
    }
}
