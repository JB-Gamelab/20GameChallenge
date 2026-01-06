using System;
using UnityEngine;

public class BlueGhostBehaviour : MonoBehaviour
{
    private GhostController ghostController;

    private void OnEnable()
    {
        ghostController.OnAvailableTilesChecked += MovementControllerOnAvailableTilesChecked;
    }

    private void OnDisable()
    {
        ghostController.OnAvailableTilesChecked -= MovementControllerOnAvailableTilesChecked;
    }

    private void Awake()
    {
        ghostController = GetComponent<GhostController>();
    }

    private void MovementControllerOnAvailableTilesChecked()
    {
        ghostController.SetDesiredDirection(MovementController.MoveDirection.Right);
    }
}
