using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmingSystem : MonoBehaviour
{
    [Header("Tilemap References")]
    public Tilemap farmingTilemap;
    public Grid grid;

    [Header("Tile References")]
    public TileBase grassTile;
    public TileBase tilledTile;

    [Header("Farming Setting")]
    public LayerMask farmingLayer;
    public float hoeRange = 1.5f;

    private static FarmingSystem instance;
    public static FarmingSystem Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (farmingTilemap == null)
        {
            farmingTilemap = GetComponent<Tilemap>();
        }

        if (grid == null)
        {
            grid = GetComponentInParent<Grid>();
        }
    }

    public bool TryHoeGround(Vector2 playerPosition, Vector2 hoeDirection)
    {
        Vector2 hoePosition = playerPosition + hoeDirection.normalized * hoeRange;

        Vector3Int cellPosition = grid.WorldToCell(hoePosition);

        TileBase currentTile = farmingTilemap.GetTile(cellPosition);

        if (currentTile == grassTile)
        {
            farmingTilemap.SetTile(cellPosition, tilledTile);

            Debug.Log($"Hoed ground at position: {cellPosition}");

            return true;
        }

        Debug.Log($"Cannot hoe at position: {cellPosition}. Current tile: {currentTile}");
        return false;
    }

    public bool CanHoeAt(Vector2 worldPosition)
    {
        Vector3Int cellPosition = grid.WorldToCell(worldPosition);
        TileBase currentTile = farmingTilemap.GetTile(cellPosition);

        return currentTile == grassTile;
    }

    public Vector2 GetHoeDirection(float lastMoveX, float lastMoveY)
    {
        Vector2 direction = new Vector2(lastMoveX, lastMoveY).normalized;

        if (direction == Vector2.zero)
        {
            direction = Vector2.down;
        }

        return direction;
    }

    public void PaintGrassTiles(BoundsInt area)
    {
        TileBase[] grassTiles = new TileBase[area.size.x * area.size.y * area.size.z];

        for (int i = 0; i < grassTiles.Length; i++)
        {
            grassTiles[i] = grassTile;
        }

        farmingTilemap.SetTilesBlock(area, grassTiles);
    }
}
