using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnInkyRelease;
    public static event Action OnClydeRelease;
    public static event Action OnBlinkySpeedUp;
    public static event Action OnBlinkySpeedUpMore;
    public static event Action OnPowerPillCollected;
    public static event Action OnPowerPillExpired;
    public static event Action OnLevelFinished;
    public static event Action OnPacmanRespawn;

    [SerializeField] private int inkyRelease = 216; //number of dots left when Inky is released
    [SerializeField] private int clydeRelease = 186; //number of dots left when Clyde is released
    [SerializeField] private int blinkyFaster = 20; //number of dots left when Blinky speeds up
    [SerializeField] private int blinkyFastest = 10; // number of dots left when Blinky speeds up again
    [SerializeField] private int scaredTime = 10;
    [SerializeField] private int lives = 3;
    [SerializeField] private GameObject pacman;

    private int dotCount;
    private int pillCount;
    private int totalCount;
    private Coroutine scaredTimerCoroutine;

    private DotGeneration dotGeneration;

    private void OnEnable()
    {
        DotController.OnDotCollected += DotControllerOnDotCollected;
        PowerPillController.OnPillCollected += PowerPillControllerOnPillCollected;
        PlayerController.OnGhostEaten += GhostEaten;
        PlayerController.OnPacmanEaten += PacmanEaten;
    }

    private void OnDisable()
    {
        DotController.OnDotCollected -= DotControllerOnDotCollected;
        PowerPillController.OnPillCollected -= PowerPillControllerOnPillCollected;
        PlayerController.OnGhostEaten -= GhostEaten;
        PlayerController.OnPacmanEaten -= PacmanEaten;
    }

    private void Awake()
    {
        dotGeneration = gameObject.GetComponent<DotGeneration>();
    }

    private void Start()
    {
        dotCount = dotGeneration.GetDotCount();
        pillCount = dotGeneration.GetPillCount();
        totalCount = dotCount + pillCount;
    }

    private void DotControllerOnDotCollected()
    {
        dotCount--;

        CheckGameState();
    }

    private void PowerPillControllerOnPillCollected()
    {
        pillCount--;
        
        OnPowerPillCollected?.Invoke();

        if (scaredTimerCoroutine != null)
        {
            StopCoroutine(scaredTimerCoroutine);    
        }
        
        scaredTimerCoroutine = StartCoroutine(ScaredTimer());


        CheckGameState();
    }

    private void CheckGameState()
    {
        if (totalCount <= inkyRelease)
        {
            OnInkyRelease?.Invoke();
        }

        if (totalCount <= clydeRelease)
        {
            OnClydeRelease?.Invoke();
        }

        if (totalCount <= blinkyFaster)
        {
            OnBlinkySpeedUp?.Invoke();
        }

        if (totalCount <= blinkyFastest)
        {
            OnBlinkySpeedUpMore?.Invoke();
        }

        if (totalCount == 0)
        {
            OnLevelFinished?.Invoke();
        }
    }

    private IEnumerator ScaredTimer()
    {
        yield return new WaitForSeconds(scaredTime);
        OnPowerPillExpired?.Invoke();
        scaredTimerCoroutine = null;
    }

    private void GhostEaten(GhostController eatenGhost)
    {
        //add points
    }

    private void PacmanEaten()
    {
        lives--;
        if (lives < 1)
        {
            //Gameover
        }
        pacman.SetActive(false);
        StartCoroutine(RestartTimer());
    }

    private IEnumerator RestartTimer()
    {
        yield return new WaitForSeconds(3);
        pacman.transform.position = new Vector3(0.5f, -2, 0);
        pacman.SetActive(true);
        OnPacmanRespawn?.Invoke();
    }
}
