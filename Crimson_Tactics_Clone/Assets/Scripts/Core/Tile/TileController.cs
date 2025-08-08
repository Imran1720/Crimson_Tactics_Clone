using CrimsonTactics.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private Vector3 tileWorldPosition;
    private Vector2 tileGridPosition;
    private TileType currentTileType;

    private void Start()
    {
        tileGridPosition = new Vector2(transform.position.x, transform.position.z);
        tileWorldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public Vector3 GetTileWorldPosition() => tileWorldPosition;
    public Vector2 GetTileGridPosition() => tileGridPosition;
    public bool IsTileOccupied() => currentTileType == TileType.OBSTACLE;
    public void ToggleTileStatus()
    {
        currentTileType = IsTileOccupied() ? TileType.FREE : TileType.OBSTACLE;
    }

}
