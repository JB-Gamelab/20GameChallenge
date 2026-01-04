using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DotGeneration : MonoBehaviour
{
    [SerializeField] private Tilemap dotTileMap;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private TileBase dotTile;
    [SerializeField] private TileBase powerUpTile;

    private BoundsInt bounds;
    private int dotCount = 0;

    private void Start()
    {
        dotCount = CalculateDots();
    }

    private int CalculateDots()
    {
        int dots = 0;

        bounds = dotTileMap.cellBounds;

        for (int yPos = bounds.yMin; yPos < bounds.yMax + 1; yPos++)
        {
            for (int xPos = bounds.xMin; xPos < bounds.xMax + 1; xPos++)
            {
                Vector3Int cellPosition = new Vector3Int(xPos, yPos, bounds.z);

                TileBase tile = dotTileMap.GetTile(cellPosition);

                if (tile == null)
                {
                    
                } else
                {
                    Vector3 worldPosition = dotTileMap.GetCellCenterWorld(cellPosition);
                                                          
                    if (tile == dotTile)
                    {
                        Instantiate(dotPrefab, worldPosition, Quaternion.identity, this.transform);
                    }

                    if (tile == powerUpTile)
                    {
                        Instantiate(powerUpPrefab, worldPosition, Quaternion.identity, this.transform);
                    }

                    dots++;
                }
            }
        }
        dotTileMap.gameObject.SetActive(false);
        
        return dots;
    }
}
