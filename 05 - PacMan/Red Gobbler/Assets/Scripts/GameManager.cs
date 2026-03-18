using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnInkyRelease;
    public static event Action OnClydeRelease;
    public static event Action OnBlinkySpeedUp;
    public static event Action OnBlinkySpeedUpMore;
    public static event Action OnPowerPillCollected;
    public static event Action OnLevelFinished;

    [SerializeField] private int inkyRelease = 212; //number of dots left when Inky is released
    [SerializeField] private int clydeRelease = 182; //number of dots left when Clyde is released
    [SerializeField] private int blinkyFaster = 20; //number of dots left when Blinky speeds up
    [SerializeField] private int blinkyFastest = 10; // number of dots left when Blinky speeds up again

    private int dotCount;
    private int pillCount;

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

        CheckGameState();
    }

    private void CheckGameState()
    {
        if (dotCount <= inkyRelease)
        {
            OnInkyRelease?.Invoke();
        }

        if (dotCount <= clydeRelease)
        {
            OnClydeRelease?.Invoke();
        }

        if (dotCount <= blinkyFaster)
        {
            OnBlinkySpeedUp?.Invoke();
        }

        if (dotCount <= blinkyFastest)
        {
            OnBlinkySpeedUpMore?.Invoke();
        }

        if (dotCount == 0 && pillCount == 0)
        {
            OnLevelFinished?.Invoke();
        }
    }
}
