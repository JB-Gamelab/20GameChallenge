using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip playerFire;
    [SerializeField] private AudioClip asteroidDestroy;
    [SerializeField] private AudioClip playerDie;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip powerUp;
    [SerializeField] private AudioClip playerThrust;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;

    //private float musicVolume;
    //private float sFXVolume;

    private void Awake()
    {
        AsteroidController.onAsteroidDestroyed += AsteroidControllerOnAsteroidController;
        PlayerMovement.onPlayerThrust += PlayerMovementOnPlayerThrust;
        PlayerFire.onPlayerFire += PlayerFireOnPlayerFire;
        PlayerController.onPlayerDeath += PlayerControllerOnPlayerDeath;
        GameManager.onGameOver += GameManagerOnGameOver;
        //musicVolume = PlayerPrefsManager.GetMusicVolume();
        //sFXVolume = PlayerPrefsManager.GetSFXVolume();        
    }

    private void OnDestroy()
    {
        AsteroidController.onAsteroidDestroyed -= AsteroidControllerOnAsteroidController;
        PlayerMovement.onPlayerThrust -= PlayerMovementOnPlayerThrust;
        PlayerFire.onPlayerFire -= PlayerFireOnPlayerFire;
        PlayerController.onPlayerDeath -= PlayerControllerOnPlayerDeath;
    }

    private void Start()
    {
        //musicSource.volume = musicVolume;
        //sFXSource.volume = sFXVolume;
        musicSource.clip = music;
        musicSource.Play();
    }

    private void PlayerMovementOnPlayerThrust()
    {
        sFXSource.PlayOneShot(playerThrust);
    }

    private void PlayerFireOnPlayerFire()
    {
        sFXSource.PlayOneShot(playerFire);
    }

    private void PlayerControllerOnPlayerDeath()
    {
        sFXSource.PlayOneShot(playerDie);
    }

    private void AsteroidControllerOnAsteroidController(int arg1, GameObject object1, GameObject object2, GameObject object3)
    {
        sFXSource.PlayOneShot(asteroidDestroy);
    }

    private void GameManagerOnGameOver()
    {
        sFXSource.PlayOneShot(gameOver);
        musicSource.Pause();
    }
}
