using CrimsonTactics.Level;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    public class TileController : MonoBehaviour
    {
        [SerializeField] private TileType currentTileType;

        private Vector2Int tileGridPosition;
        private Vector3 tileWorldPosition;

        private void Start()
        {
            tileGridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
            tileWorldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        public void ToggleTileStatus()
        {
            currentTileType = IsTileOccupied() ? TileType.FREE : TileType.OBSTACLE;
        }

        public Vector2Int GetTileGridPosition() => tileGridPosition;
        public Vector3 GetTileWorldPosition() => tileWorldPosition;
        public TileType GetTileType() => currentTileType;
        public bool IsTileOccupied() => currentTileType == TileType.OBSTACLE;
    }
}