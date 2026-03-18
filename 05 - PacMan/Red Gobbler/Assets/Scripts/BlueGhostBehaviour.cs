using System;
using Unity.VisualScripting;
using UnityEngine;

public class BlueGhostBehaviour : GhostBehaviour
{
    [SerializeField] private Transform playerTransform;

    private bool ghostStart = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.OnInkyRelease += GameManagerOnInkyRelease;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.OnInkyRelease -= GameManagerOnInkyRelease;
    }

    private void GameManagerOnInkyRelease()
    {
        ghostStart = true;
    }

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

    public override bool StartGhost()
    {
        return ghostStart;
    }
}
