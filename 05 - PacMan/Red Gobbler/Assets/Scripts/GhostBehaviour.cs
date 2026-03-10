using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostBehaviour : MonoBehaviour
{
      public abstract Vector3Int GetTargetTile();

      public MovementController.MoveDirection ChooseDirection(List<MovementController.MoveDirection> moveOptions, MovementController.MoveDirection currentDirection, Vector3Int currentPosition)
      {
            Vector3Int targetCell = GetTargetTile();

            float shortest = float.MaxValue;
            MovementController.MoveDirection bestMove = moveOptions[0];

            foreach (MovementController.MoveDirection dir in moveOptions)
            {
                  Vector3Int nextCell = GetNextCell(currentPosition, currentDirection);
                  float distance = Vector3Int.Distance(nextCell, targetCell);

                  if (distance < shortest)
                  {
                        shortest = distance;
                        bestMove = dir;
                  }
            }

            return bestMove;
      }

      private Vector3Int GetNextCell(Vector3Int currentPos, MovementController.MoveDirection direction)
      {
            Vector3Int nextCell = currentPos;

            if (direction == MovementController.MoveDirection.Left)
            {
                  nextCell = currentPos + new Vector3Int(-1, 0, 0);
            }
            if (direction == MovementController.MoveDirection.Right)
            {
                  nextCell = currentPos + new Vector3Int(1, 0, 0);
            }
            if (direction == MovementController.MoveDirection.Up)
            {
                  nextCell = currentPos + new Vector3Int(0, 1, 0);
            }
            if (direction == MovementController.MoveDirection.Down)
            {
                  nextCell = currentPos + new Vector3Int(0, -1, 0);
            }

            return nextCell;
      }
}