using System;
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

    private MovementController.MoveDirection desiredDirection;
    private MovementController.MoveDirection currentDirection;

    private Vector3Int ghostCellPosition;
    private Vector3Int leftAdd = new Vector3Int(-1, 0, 0);
    private Vector3Int rightAdd = new Vector3Int(1, 0, 0);
    private Vector3Int topAdd = new Vector3Int(0, 1, 0);
    private Vector3Int bottomAdd = new Vector3Int(0, -1, 0);
    private Vector3Int[] possibleMovementCells = new Vector3Int[4];
    private bool leftCell;
    private bool rightCell;
    private bool topCell;
    private bool bottomCell;

    private bool[] availableCells = new bool[4];

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
            GetPossibleDirections(ghostCellPosition);
            SetDesiredDirection(CalculateDesiredDirection());
            
            movementController.Move(desiredDirection);
        }
    }

    private void GetPossibleDirections(Vector3Int currentCellPosition) // Check each cell surrounding the current intersection cell
    {
        //Add all 4 movedirection cells to array
        possibleMovementCells[0] = currentCellPosition + leftAdd;
        possibleMovementCells[1] = currentCellPosition + rightAdd;
        possibleMovementCells[2] = currentCellPosition + topAdd;
        possibleMovementCells[3] = currentCellPosition + bottomAdd;

        for (int i = 0; i < availableCells.Length; i++)
        {
            availableCells[i] = false;
        }

        //Check left cell, ignore if moving right
        if (floorTileMap.HasTile(possibleMovementCells[0]) && currentDirection != MovementController.MoveDirection.Right)
        {
            leftCell = true;
        } else
        {
            leftCell = false;
        }

        //Check right cell, ignore if mopving left
        if (floorTileMap.HasTile(possibleMovementCells[1]) && currentDirection != MovementController.MoveDirection.Left)
        {
            rightCell = true;
        } else
        {
            rightCell = false;
        }
        
        //Check top cell, ignore if moving down
        if (floorTileMap.HasTile(possibleMovementCells[2]) && currentDirection != MovementController.MoveDirection.Down)
        {
            topCell = true;
        } else
        {
            topCell = false;
        }

        //Check bottom cell, ignore if moving up
        if (floorTileMap.HasTile(possibleMovementCells[3]))
        {
            bottomCell = true;
        } else
        {
            bottomCell = false;
        }
        

        availableCells[0] = leftCell;
        availableCells[1] = rightCell;
        availableCells[2] = topCell;
        availableCells[3] = bottomCell;

        OnAvailableTilesChecked?.Invoke();
    }

    private void MovementControllerOnDirectionChanged(MovementController.MoveDirection direction)
    {
        currentDirection = desiredDirection;
        Debug.Log(currentDirection);
    }

    private void Start()
    {
        desiredDirection = MovementController.MoveDirection.Up;
    }

    private void Update()
    {        
        movementController.Move(desiredDirection);
    }

    private MovementController.MoveDirection CalculateDesiredDirection()
    {
        Vector3Int targetCell = ghostBehaviour.GetTargetTile();
        MovementController.MoveDirection shortestMove = MovementController.MoveDirection.Stopped;
        int moveDistance = 20;

        for (int i = 0; i < availableCells.Length; i++)
        {
            if (availableCells[i])
            {
                int testDistance = MeasureDistance(possibleMovementCells[i], targetCell);

                if (testDistance < moveDistance)
                {
                    moveDistance = testDistance;
                    if (i == 0)
                    {
                        shortestMove = MovementController.MoveDirection.Left;
                    }
                    if (i == 1)
                    {
                        shortestMove = MovementController.MoveDirection.Right;
                    }
                    if (i == 2)
                    {
                        shortestMove = MovementController.MoveDirection.Up;
                    }
                    if (i == 3)
                    {
                        shortestMove = MovementController.MoveDirection.Down;
                    }
                }
            }
        }
        return shortestMove;
    }

    private void SetDesiredDirection(MovementController.MoveDirection direction)
    {
        desiredDirection = direction;
    }

    private int MeasureDistance(Vector3Int testCell, Vector3Int targetCell)
    {
        int testDistance = 0;

        int dX = Mathf.Abs(testCell.x - targetCell.x);
        int dY = Mathf.Abs(testCell.y - targetCell.y);

        testDistance = dX + dY;

        return testDistance;
    }
}
