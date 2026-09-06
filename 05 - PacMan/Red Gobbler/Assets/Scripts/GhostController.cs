using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Tilemap intersectionTileMap;
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private Tilemap ghostSpawnTileMap;
    [SerializeField] private float spawnDelay = 2;
    private GhostBehaviour ghostBehaviour;
    private GhostSpriteController spriteController;
    private MovementController movementController;

    private MovementController.MoveDirection currentDirection;
    private MovementController.MoveDirection desiredDirection;
    private GhostBehaviour.GhostState currentState;

    private Vector3Int ghostCellPosition;

    private bool isScared = false;
    private bool isEaten = false;

    private void Awake()
    {
        ghostBehaviour = GetComponent<GhostBehaviour>();
        if (ghostBehaviour == null)
        {
            Debug.Log("No behaviour AI attached");   
        }
        movementController = GetComponent<MovementController>();
        spriteController = GetComponent<GhostSpriteController>();
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
        movementController.SetMoveSpeed(ghostBehaviour.GetGhostSpeed(ghostState));

        if (ghostState != GhostBehaviour.GhostState.Waiting)
        {
            MoveCheck();
        }

        if (ghostState == GhostBehaviour.GhostState.Scared)
        {
            isScared = true;
        } 
        else
        {
            isScared = false;
        }

        if (ghostState == GhostBehaviour.GhostState.Eaten)
        {
            isEaten = true;
            isScared = false;
        }
        else
        {
            isEaten = false;
        }

        currentState = ghostState;
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
        if (!isScared)
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

            if (isEaten && ghostSpawnTileMap.HasTile(ghostCellPosition))
            {                
                ghostBehaviour.ChangeGhostState(GhostBehaviour.GhostState.Dead);
                movementController.Move(MovementController.MoveDirection.Stopped);
                StartCoroutine(SpawnTimer());
            }
        }
        else
        {
            desiredDirection = ghostBehaviour.Opposite(currentDirection);
            movementController.Move(desiredDirection);
            isScared = false;
        }
    }

    private void MovementControllerOnDirectionChanged(MovementController.MoveDirection direction)
    {
        Debug.Log(direction);
        if (direction == MovementController.MoveDirection.Stopped)
        {
            if (!isEaten)
            {
                Debug.Log("Test");
            }
        } else
        {
            currentDirection = movementController.GetCurrentMoveDirection();
        }        
    }

    private void Update()
    {
        if (ghostBehaviour.ghostState != GhostBehaviour.GhostState.Waiting)
        {
            movementController.Move(desiredDirection);
        }
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnDelay);
        ghostBehaviour.ChangeGhostState(GhostBehaviour.GhostState.Chasing);
        spriteController.GhostNormal();
    }
}
