using UnityEngine;

public class ExtraSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip menuMusic;

        private void Start()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        musicSource.clip = menuMusic;
        musicSource.Play();
    } 
}
