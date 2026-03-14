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
    [SerializeField] private Tilemap intersectionTileMap;
    [SerializeField] private Tilemap floorTileMap;
    private GhostBehaviour ghostBehaviour;

    private MovementController movementController;

    private MovementController.MoveDirection currentDirection;
    private MovementController.MoveDirection desiredDirection;

    private Vector3Int ghostCellPosition;

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
        ghostBehaviour.OnStateChanged += GhostBehaviourOnStateChanged;
    }

    private void OnDisable()
    {
        movementController.OnDirectionChanged -= MovementControllerOnDirectionChanged;
        movementController.OnCentreSnap -= MovementControllerOnCentreSnap;
        ghostBehaviour.OnStateChanged -= GhostBehaviourOnStateChanged;
    }

    private void MovementControllerOnCentreSnap()
    {
        MoveCheck();
    }

    private void GhostBehaviourOnStateChanged(GhostBehaviour.GhostState ghostState)
    {
        if (ghostState != GhostBehaviour.GhostState.Waiting)
        {
            MoveCheck();
        }
    }

    private List<MovementController.MoveDirection> GetPossibleDirections(Vector3Int currentCellPosition)
    {
        List<MovementController.MoveDirection> options = new List<MovementController.MoveDirection>();

        if (floorTileMap.HasTile(currentCellPosition + Vector3Int.up))
        {
            options.Add(MovementController.MoveDirection.Up);
        }

        if (floorTileMap.HasTile(currentCellPosition + Vector3Int.right))
        {
            options.Add(MovementController.MoveDirection.Right);
        }

        if (floorTileMap.HasTile(currentCellPosition + Vector3Int.down))
        {
            options.Add(MovementController.MoveDirection.Down);
        }

        if (floorTileMap.HasTile(currentCellPosition + Vector3Int.left))
        {
            options.Add(MovementController.MoveDirection.Left);
        }
           
        return options;
    }

    private void MoveCheck()
    {
        ghostCellPosition = floorTileMap.WorldToCell(transform.position);

        if (intersectionTileMap.HasTile(ghostCellPosition))
        {
            List<MovementController.MoveDirection> moveOptions = GetPossibleDirections(ghostCellPosition);
            
             desiredDirection = ghostBehaviour.ChooseDirection(moveOptions, currentDirection, ghostCellPosition);         
             movementController.Move(desiredDirection);
        } else
        {
            desiredDirection = currentDirection;
            movementController.Move(desiredDirection);
        }
    }

    private void MovementControllerOnDirectionChanged(MovementController.MoveDirection direction)
    {
        if (direction == MovementController.MoveDirection.Stopped)
        {
            MoveCheck();
        } else
        {
            currentDirection = movementController.GetCurrentMoveDirection();
        }        
    }

    private void Update()
    {
        movementController.Move(desiredDirection);
    }
}
