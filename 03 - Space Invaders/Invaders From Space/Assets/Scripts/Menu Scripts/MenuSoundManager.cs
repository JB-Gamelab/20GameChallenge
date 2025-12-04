using System;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip menuClick;
    [SerializeField] private AudioClip sliderMove;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private StartMenuManager startMenuManager;

    
    private void Awake()
    {
        StartMenuManager.onClick += StartMenuManagerOnClick;
    }

    private void OnDestroy()
    {
        StartMenuManager.onClick -= StartMenuManagerOnClick;
    }

    private void Start()
    {
        musicSource.volume = startMenuManager.GetMusicVolume();
        sFXSource.volume = startMenuManager.GetSFXVolume();
        musicSource.clip = menuMusic;
        musicSource.Play();
    }    

    private void StartMenuManagerOnClick()
    {
        sFXSource.PlayOneShot(menuClick);
    }

    public void OnMusicVolumeSliderChange()
    {
        musicSource.volume = startMenuManager.GetMusicVolume();
    }

    public void OnSFXVolumeSliderChange()
    {
        sFXSource.volume = startMenuManager.GetSFXVolume();
        sFXSource.PlayOneShot(sliderMove);
    }
}
