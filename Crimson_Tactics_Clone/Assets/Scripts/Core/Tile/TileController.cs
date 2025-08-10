using CrimsonTactics.Level;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    public class TileController : MonoBehaviour
    {
        [SerializeField] private TileType currentTileType;

        private Vector3Int tileGridPosition;

        private void Start()
        {
            tileGridPosition = CalculateGridPosition();
        }

        private Vector3Int CalculateGridPosition()
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            int z = (int)transform.position.z;

            return new Vector3Int(x, y, z);
        }

        public void ToggleTileStatus()
        {
            currentTileType = IsTileOccupied() ? TileType.FREE : TileType.OBSTACLE;
        }

        public TileType GetTileType() => currentTileType;
        public Vector3 GetTileWorldPosition() => transform.position;
        public Vector3Int GetTileGridPosition() => tileGridPosition;
        public bool IsTileOccupied() => currentTileType == TileType.OBSTACLE;
    }
}