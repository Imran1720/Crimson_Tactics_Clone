using CrimsonTactics.Level;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    public class TileController : MonoBehaviour
    {
        private Vector2 tileGridPosition;
        private Vector3 tileWorldPosition;

        private TileType currentTileType;

        private void Start()
        {
            tileGridPosition = new Vector2(transform.position.x, transform.position.z);
            tileWorldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        public void ToggleTileStatus()
        {
            currentTileType = IsTileOccupied() ? TileType.FREE : TileType.OBSTACLE;
        }

        public Vector2 GetTileGridPosition() => tileGridPosition;
        public Vector3 GetTileWorldPosition() => tileWorldPosition;

        public bool IsTileOccupied() => currentTileType == TileType.OBSTACLE;
    }
}