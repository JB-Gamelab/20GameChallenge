using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
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

    [SerializeField] private int inkyRelease = 216; //number of dots left when Inky is released
    [SerializeField] private int clydeRelease = 186; //number of dots left when Clyde is released
    [SerializeField] private int blinkyFaster = 20; //number of dots left when Blinky speeds up
    [SerializeField] private int blinkyFastest = 10; // number of dots left when Blinky speeds up again
    [SerializeField] private int scaredTime = 10;

    private int dotCount;
    private int pillCount;
    private int totalCount;

    private DotGeneration dotGeneration;

    private void OnEnable()
    {
        DotController.OnDotCollected += DotControllerOnDotCollected;
        PowerPillController.OnPillCollected += PowerPillControllerOnPillCollected;
    }

    private void OnDisable()
    {
        DotController.OnDotCollected -= DotControllerOnDotCollected;
        PowerPillController.OnPillCollected -= PowerPillControllerOnPillCollected;
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

        StartCoroutine(ScaredTimer());

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
    }
}
