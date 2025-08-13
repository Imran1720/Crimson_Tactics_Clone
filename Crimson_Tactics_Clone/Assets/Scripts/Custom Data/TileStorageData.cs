using CrimsonTactics.Level;
using System;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    //This data type is used to get tile type and position
    //( Helpfull in pathfinding with obstacle detection)

    [Serializable]
    public class TileStorageData
    {
        public TileType tileType;
        public Vector3Int gridPosition;

        public TileStorageData(Vector3Int gridPosition, TileType tileType)
        {
            this.gridPosition = gridPosition;
            this.tileType = tileType;
        }

        public void SetTileType(TileType type) => tileType = type;
        public TileType GetTileType() => tileType;
        public Vector3Int GetGridPosition() => gridPosition;
    }
}
