using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    public event Action<MoveDirection> OnDirectionChanged;
    public event Action OnCentreSnap;

    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private Tilemap teleportTileMap;
    [SerializeField] private float cellCentreOffset = 0.05f;
    [SerializeField] private float moveSpeed = 5f;

    private MoveDirection actualMoveDirection;
    private MoveDirection currentDirection;
    private Vector3Int spriteCellPosition;
    private Vector3Int currentSpriteCellPosition;
    private Vector3 spriteCellWorldPosition;
    private Vector3 directionVector;

    public void Move(MoveDirection desiredDirection)
    {
        spriteCellPosition = floorTileMap.WorldToCell(transform.position);
        spriteCellWorldPosition = floorTileMap.GetCellCenterWorld(spriteCellPosition);

        CellSnap(spriteCellPosition, spriteCellWorldPosition);

        if(transform.position == spriteCellWorldPosition)
        {
            actualMoveDirection = CheckMovementAllowed(currentDirection, desiredDirection);
        }
        directionVector = SetDirectionVector(actualMoveDirection);    

        transform.position += directionVector * moveSpeed * Time.deltaTime;
    }

    private void CellSnap(Vector3Int cellPosition, Vector3 cellWorldPosition)
    {
        if (Vector3.Distance(transform.position, cellWorldPosition) < cellCentreOffset)
        {
            if (cellPosition != currentSpriteCellPosition)
            {
                currentSpriteCellPosition = cellPosition;
                transform.position = cellWorldPosition;

                if (teleportTileMap.HasTile(cellPosition))
                {
                    // The maze is fixed and symmetrical around the x axis, teleport by mirroring x
                    transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
                    currentSpriteCellPosition = floorTileMap.WorldToCell(transform.position);
                }

                OnCentreSnap?.Invoke();
            }
        }
    }

    private MoveDirection CheckMovementAllowed(MoveDirection thisCurrentDirection, MoveDirection desiredDirection)
    {
        MoveDirection moveDirection = thisCurrentDirection;

        if(desiredDirection != thisCurrentDirection)
        {
            Vector3Int desiredVector = SetDirectionVector(desiredDirection);
            Vector3Int testCell = spriteCellPosition + desiredVector;

            if(floorTileMap.GetTile(testCell) != null)
            {
                moveDirection = desiredDirection;
                currentDirection = desiredDirection;                
            } else
            {
                moveDirection = thisCurrentDirection;
            }
        } else if(desiredDirection == thisCurrentDirection)
        {
            Vector3Int currentVector = SetDirectionVector(thisCurrentDirection);
            Vector3Int testCell = spriteCellPosition + currentVector;

            if(floorTileMap.GetTile(testCell) == null)
            {
                moveDirection = MoveDirection.Stopped;
            }
        }
        OnDirectionChanged?.Invoke(moveDirection);
        return moveDirection;             
    }

    public Vector3Int SetDirectionVector(MoveDirection newDirection)
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

    public enum MoveDirection
    {
        Right,
        Left,
        Up,
        Down,
        Stopped
    }
}
