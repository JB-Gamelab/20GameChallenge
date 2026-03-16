using System;
using Unity.VisualScripting;
using UnityEngine;

public class BlueGhostBehaviour : GhostBehaviour
{
    [SerializeField] private Transform playerTransform;

    public override Vector3Int GetTargetTile(GhostState state)
    {
        switch(state)
        {
            case GhostState.Chasing:
            return Vector3Int.RoundToInt(playerTransform.position);

            case GhostState.Scattering:
            return Vector3Int.RoundToInt(cornerTransform.position);

            case GhostState.Eaten:
            return Vector3Int.RoundToInt(ghostStartTransform.position);

            case GhostState.Waiting:
            return Vector3Int.zero;

            case GhostState.Scared:
            return Vector3Int.RoundToInt(cornerTransform.position);
        }

        return Vector3Int.zero;
    }
}
