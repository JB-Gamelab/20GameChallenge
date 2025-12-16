using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider sFXVolSlider;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicVolSlider.value = PlayerPrefsManager.GetMusicVolume();
            sFXVolSlider.value = PlayerPrefsManager.GetSFXVolume();
        }
    }

    public void OnStart()
    {
        SceneManager.LoadScene(1);
        PlayerPrefsManager.SetVolumes(musicVolSlider.value, sFXVolSlider.value);
    }

    public void OnScores()
    {
        SceneManager.LoadScene(2);
        PlayerPrefsManager.SetVolumes(musicVolSlider.value, sFXVolSlider.value);
    }

    public void OnCredits()
    {
        SceneManager.LoadScene(3);
        PlayerPrefsManager.SetVolumes(musicVolSlider.value, sFXVolSlider.value);
    }

    public void OnExit()
    {
        SceneManager.LoadScene(0);
    }

    public float GetMusicVolume()
    {
        return musicVolSlider.value;
    }
}
