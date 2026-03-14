using System;
using UnityEngine;

public class BlueGhostBehaviour : GhostBehaviour
{
    [SerializeField] private Transform playerTransform;

    public override Vector3Int GetTargetTile()
    {
        switch(ghostState)
        {
            case GhostState.Chasing:
            return Vector3Int.RoundToInt(playerTransform.position);

            case GhostState.Scattering:
            return Vector3Int.RoundToInt(cornerTransform.position);

            case GhostState.Eaten:
            return Vector3Int.RoundToInt(ghostStartTransform.position);
        }

        return new Vector3Int(0, 0, 0);
    }
}
