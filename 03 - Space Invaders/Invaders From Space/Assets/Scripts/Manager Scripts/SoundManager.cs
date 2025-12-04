using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip enemyFire;
    [SerializeField] private AudioClip playerFire;
    [SerializeField] private AudioClip enemyDie;
    [SerializeField] private AudioClip playerDie;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;

    private float musicVolume;
    private float sFXVolume;

    private void Awake()
    {
        PlayerController.onPlayerDeath += PlayerControllerOnPlayerDeath;
        PlayerController.onPlayerFire += PlayerControllerOnPlayerFire;
        EnemyController.onEnemyDestroyed += EnemyControllerOnEnemyDestroyed;
        EnemyFire.onEnemyFire += EnemyFireOnEnemyFire;
        GameManager.onGameOver += GameManagerOnGameOver;
        musicVolume = PlayerPrefsManager.GetMusicVolume();
        sFXVolume = PlayerPrefsManager.GetSFXVolume();
    }

    private void OnDestroy()
    {
        PlayerController.onPlayerDeath -= PlayerControllerOnPlayerDeath;
        PlayerController.onPlayerFire -= PlayerControllerOnPlayerFire;
        EnemyController.onEnemyDestroyed -= EnemyControllerOnEnemyDestroyed;
        EnemyFire.onEnemyFire -= EnemyFireOnEnemyFire;
        GameManager.onGameOver -= GameManagerOnGameOver;
    }

    private void Start()
    {
        musicSource.volume = musicVolume;
        sFXSource.volume = sFXVolume;
        musicSource.clip = music;
        musicSource.Play();
    }

    private void GameManagerOnGameOver()
    {
        musicSource.Stop();
        sFXSource.PlayOneShot(gameOver);        
    }

    private void EnemyFireOnEnemyFire()
    {
        sFXSource.PlayOneShot(enemyFire);
    }

    private void EnemyControllerOnEnemyDestroyed(Transform transform)
    {
        sFXSource.PlayOneShot(enemyDie);
    }

    private void PlayerControllerOnPlayerFire()
    {
        sFXSource.PlayOneShot(playerFire);
    }

    private void PlayerControllerOnPlayerDeath()
    {
        sFXSource.PlayOneShot(playerDie);
    }
}
