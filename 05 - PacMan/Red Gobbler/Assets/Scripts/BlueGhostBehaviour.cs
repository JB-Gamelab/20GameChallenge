using System;
using UnityEngine;

public class BlueGhostBehaviour : GhostBehaviour
{
    [SerializeField] private Transform playerTransform;

    public override Vector3Int GetTargetTile()
    {
        return Vector3Int.RoundToInt(playerTransform.position);
    }
}
