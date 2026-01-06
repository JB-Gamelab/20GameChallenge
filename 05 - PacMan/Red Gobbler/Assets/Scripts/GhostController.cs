using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostController : MonoBehaviour
{
    public event Action OnAvailableTilesChecked;

    [SerializeField] private Tilemap intersectionTileMap;
    [SerializeField] private Tilemap floorTileMap;

    private MovementController movementController;

    private MovementController.MoveDirection desiredDirection;

    private Vector3Int ghostCellPosition;
    private Vector3Int leftAdd = new Vector3Int(-1, 0, 0);
    private Vector3Int rightAdd = new Vector3Int(1, 0, 0);
    private Vector3Int topAdd = new Vector3Int(0, 1, 0);
    private Vector3Int bottomAdd = new Vector3Int(0, -1, 0);
    private bool leftCell;
    private bool rightCell;
    private bool topCell;
    private bool bottomCell;

    private bool[] availableCells = new bool[4];

    private void Awake()
    {
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
        }
    }

    private void GetPossibleDirections(Vector3Int currentCellPosition) // Check each cell surrounding the current intersection cell
    {
        //Check left cell
        if (floorTileMap.HasTile(currentCellPosition + leftAdd))
        {
            leftCell = true;
        } else
        {
            leftCell = false;
        }
        //Check right cell
        if (floorTileMap.HasTile(currentCellPosition + rightAdd))
        {
            rightCell = true;
        } else
        {
            rightCell = false;
        }
        //Check top cell
        if (floorTileMap.HasTile(currentCellPosition + topAdd))
        {
            topCell = true;
        } else
        {
            topCell = false;
        }
        //Check bottom cell
        if (floorTileMap.HasTile(currentCellPosition + bottomAdd))
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
        //desiredDirection = direction;
    }

    private void Start()
    {
        desiredDirection = MovementController.MoveDirection.Up;
    }

    private void Update()
    {        
        movementController.Move(desiredDirection);
        Debug.Log(desiredDirection);
    }

    public bool[] GetAvailableCells()
    {
        return availableCells;
    }

    public void SetDesiredDirection(MovementController.MoveDirection direction)
    {
        desiredDirection = direction;
    }
}
