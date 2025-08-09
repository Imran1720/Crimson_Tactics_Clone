using CrimsonTactics.Level;
using System;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    [Serializable]
    public class TileStorageData
    {
        public Vector3Int gridPosition;
        public TileType tileType;

        public TileStorageData(Vector3Int gridPosition, TileType tileType)
        {
            this.gridPosition = gridPosition;
            this.tileType = tileType;
        }
    }
}
