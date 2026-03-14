using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostBehaviour : MonoBehaviour
{
      public event Action<GhostState> OnStateChanged;

      [SerializeField] public Transform cornerTransform;
      [SerializeField] public Transform ghostStartTransform;

      public GhostState ghostState;
     
      public abstract Vector3Int GetTargetTile();

    private void Start()
    {
        ghostState = GhostState.Waiting;
    }

    public MovementController.MoveDirection ChooseDirection(List<MovementController.MoveDirection> moveOptions, MovementController.MoveDirection currentDirection, Vector3Int currentPosition)
      {
            Vector3Int targetCell = GetTargetTile();

            float shortest = float.MaxValue;

            //Only do this when backwards movement NOT allowed
            MovementController.MoveDirection reverse = Opposite(currentDirection);
            moveOptions.Remove(reverse);

            MovementController.MoveDirection bestMove = moveOptions[0];

            foreach (MovementController.MoveDirection direction in moveOptions)
            {
                  Vector3Int nextCell = GetNextCell(currentPosition, direction);
                  float distance = Vector3Int.Distance(nextCell, targetCell);

                  if (distance < shortest)
                  {
                        shortest = distance;
                        bestMove = direction;
                  }
            }

            return bestMove;
      }

      private MovementController.MoveDirection Opposite(MovementController.MoveDirection direction)
      {
            switch (direction)
            {
                  case MovementController.MoveDirection.Left: return MovementController.MoveDirection.Right;
                  case MovementController.MoveDirection.Right: return MovementController.MoveDirection.Left;
                  case MovementController.MoveDirection.Up: return MovementController.MoveDirection.Down;
                  case MovementController.MoveDirection.Down: return MovementController.MoveDirection.Up;
                  default: return MovementController.MoveDirection.Stopped;
            }
      }

      private Vector3Int GetNextCell(Vector3Int currentPos, MovementController.MoveDirection direction)
      {
            Vector3Int nextCell = currentPos;

            if (direction == MovementController.MoveDirection.Left)
            {
                  nextCell = currentPos + Vector3Int.left;
            }
            if (direction == MovementController.MoveDirection.Right)
            {
                  nextCell = currentPos + Vector3Int.right;
            }
            if (direction == MovementController.MoveDirection.Up)
            {
                  nextCell = currentPos + Vector3Int.up;
            }
            if (direction == MovementController.MoveDirection.Down)
            {
                  nextCell = currentPos + Vector3Int.down;
            }

            return nextCell;
      }



      public enum GhostState
      {
            Waiting,
            Chasing,
            Scattering,
            Scared,
            Eaten
      }
}