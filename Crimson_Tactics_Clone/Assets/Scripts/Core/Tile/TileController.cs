using CrimsonTactics.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private Vector3 tilePosition;
    private TileType currentTileType;

    private void Start()
    {
        tilePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public Vector3 GetTilePosition() => tilePosition;
    public bool IsTileOccupied() => currentTileType == TileType.OBSTACLE;
    public void ToggleTileStatus()
    {
        currentTileType = IsTileOccupied() ? TileType.FREE : TileType.OBSTACLE;
    }

}
