using UnityEngine;

public abstract class GhostBehaviour : MonoBehaviour
{
    protected MovementController pacman;

    protected virtual void Awake()
    {
        pacman = FindAnyObjectByType<MovementController>();
    }

    public abstract Vector3Int GetTargetTile();
}
