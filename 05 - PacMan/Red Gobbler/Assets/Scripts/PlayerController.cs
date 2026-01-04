using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public static event Action<MoveDirection> OnDirectionChanged;

    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float cellCentreOffset = 0.05f;

    private MoveDirection currentDirection;
    private MoveDirection desiredDirection;
    private Vector3Int playerCellPosition;
    private Vector3Int currentPlayerCellPosition;
    private Vector3 playerCellWorldPosition;

    private void Start()
    {
        currentDirection = MoveDirection.Stopped;
        desiredDirection = MoveDirection.Stopped;
    }

    private void Update()
    {
        playerCellPosition = floorTileMap.WorldToCell(transform.position);
        playerCellWorldPosition = floorTileMap.GetCellCenterWorld(playerCellPosition);

        CellSnap(playerCellPosition, playerCellWorldPosition);
        CheckMovementAllowed();
        
        Vector3 directionVector = SetDirectionVector(currentDirection);    

        transform.position += directionVector * moveSpeed * Time.deltaTime;
    }

    private void CellSnap(Vector3Int playerCellPosition, Vector3 cellWorldPosition)
    {
        if (Vector3.Distance(transform.position, cellWorldPosition) < cellCentreOffset)
        {
            if (playerCellPosition != currentPlayerCellPosition)
            {
                currentPlayerCellPosition = playerCellPosition;
                transform.position = cellWorldPosition;
            }
        }
    }

    private void CheckMovementAllowed()
    {
        if(transform.position == playerCellWorldPosition)
        {
            if(desiredDirection != currentDirection)
            {
                Vector3Int desiredVector = SetDirectionVector(desiredDirection);
                Vector3Int testCell = playerCellPosition + desiredVector;

                if(floorTileMap.GetTile(testCell) != null)
                {
                    currentDirection = desiredDirection;
                    OnDirectionChanged?.Invoke(currentDirection);
                } else
                {
                    desiredDirection = currentDirection;
                }
            } else
            {
                Vector3Int currentVector = SetDirectionVector(currentDirection);
                Vector3Int testCell = playerCellPosition + currentVector;

                if(floorTileMap.GetTile(testCell) == null)
                {
                    currentDirection = MoveDirection.Stopped;
                }
            }
        }
    }

    private Vector3Int SetDirectionVector(MoveDirection newDirection)
    {
        switch (newDirection)
        {
            case MoveDirection.Up:
                
            return new Vector3Int(0, 1);

            case MoveDirection.Down:
                
            return new Vector3Int(0, -1);

            case MoveDirection.Left:
                
            return new Vector3Int(-1, 0);

            case MoveDirection.Right:
                
            return new Vector3Int(1, 0);

            case MoveDirection.Stopped:
                
            return new Vector3Int(0, 0);
        }

        return new Vector3Int(0, 0);
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredDirection = MoveDirection.Right;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredDirection = MoveDirection.Left;
        }
    }

    public void MoveUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredDirection = MoveDirection.Up;
        }
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            desiredDirection = MoveDirection.Down;
        }
    }

    public enum MoveDirection
    {
        Right,
        Left,
        Up,
        Down,
        Stopped
    }
}
