using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private Image startButton;
    [SerializeField] private Image highButton;
    [SerializeField] private Image scoresButton;
    [SerializeField] private Image creditsButton;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sFXVolume;

    public void StartGame()
    {
        PlayerPrefsManager.SetVolumes(musicVolume.value, sFXVolume.value);
        SceneManager.LoadScene(1);
    }

    public void HighScore()
    {
        SceneManager.LoadScene(2);
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }
}
