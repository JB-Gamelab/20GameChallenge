using UnityEngine;

public class StartMenuSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private StartMenuController startMenuController;

    private float musicVolume;

    private float sFXVolume;

    private void Awake()
    {
        musicVolume = PlayerPrefsManager.GetMusicVolume();
        startMenuController = GetComponent<StartMenuController>();
    }

    private void Start()
    {
        musicSource.volume = musicVolume;
        musicSource.clip = music;
        musicSource.Play();
    }

    public void OnMusicVolumeSliderChange()
    {
        musicSource.volume = startMenuController.GetMusicVolume();
    }
}
