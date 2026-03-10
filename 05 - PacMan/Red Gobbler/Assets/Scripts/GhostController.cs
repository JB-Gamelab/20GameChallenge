using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Tilemaps;

public class GhostController : MonoBehaviour
{
    public event Action OnAvailableTilesChecked;

    [SerializeField] private Tilemap intersectionTileMap;
    [SerializeField] private Tilemap floorTileMap;
    private GhostBehaviour ghostBehaviour;

    private MovementController movementController;

    private MovementController.MoveDirection currentDirection;

    private Vector3Int ghostCellPosition;
    private Vector3Int leftAdd = new Vector3Int(-1, 0, 0);
    private Vector3Int rightAdd = new Vector3Int(1, 0, 0);
    private Vector3Int upAdd = new Vector3Int(0, 1, 0);
    private Vector3Int downAdd = new Vector3Int(0, -1, 0);

    private void Awake()
    {
        ghostBehaviour = GetComponent<GhostBehaviour>();
        if (ghostBehaviour == null)
        {
            Debug.Log("No behaviour AI attached");   
        }
        movementController = GetComponent<MovementController>();
    }

    private void OnEnable()
    {
        movementController.OnDirectionChanged += MovementControllerOnDirectionChanged;
        movementController.OnCentreSnap += MovementControllerOnCentreSnap;
    }

    private void OnDisable()
    {
        movementController.OnDirectionChanged -= MovementControllerOnDirectionChanged;
        movementController.OnCentreSnap -= MovementControllerOnCentreSnap;
    }

    private void MovementControllerOnCentreSnap()
    {
        ghostCellPosition = floorTileMap.WorldToCell(transform.position);

        if (intersectionTileMap.HasTile(ghostCellPosition))
        {
            List<MovementController.MoveDirection> moveOptions = GetPossibleDirections(ghostCellPosition);
            
           // MovementController.MoveDirection desiredDirection = GhostBehaviour.ChooseDirection(moveOptions, currentDirection, ghostCellPosition);

           // movementController.Move(desiredDirection);
        }
    }

    private List<MovementController.MoveDirection> GetPossibleDirections(Vector3Int currentCellPosition)
    {
        List<MovementController.MoveDirection> options = new List<MovementController.MoveDirection>();

        if (floorTileMap.HasTile(currentCellPosition + leftAdd))
        {
            options.Add(MovementController.MoveDirection.Left);
        }

        if (floorTileMap.HasTile(currentCellPosition + rightAdd))
        {
            options.Add(MovementController.MoveDirection.Right);
        }

        if (floorTileMap.HasTile(currentCellPosition + upAdd))
        {
            options.Add(MovementController.MoveDirection.Up);
        }

        if (floorTileMap.HasTile(currentCellPosition + downAdd))
        {
            options.Add(MovementController.MoveDirection.Down);
        }

        return options;
    }

    private void MovementControllerOnDirectionChanged(MovementController.MoveDirection direction)
    {
        //currentDirection = MovementController.GetCurrentMoveDirection();
    }

    private void Start()
    {
       // desiredDirection = MovementController.MoveDirection.Up;
    }

    private void Update()
    {        
      //  movementController.Move(desiredDirection);
    }
}
