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

        public void InitializeData(Vector3Int position, TileType type, Transform container)
        {
            currentTileType = type;
            SetTilePosition(position);
            transform.SetParent(container, false);
        }

        public TileType GetTileType() => currentTileType;
        public Vector3 GetTileWorldPosition() => transform.position;
        public Vector3Int GetTileGridPosition() => tileGridPosition;
        public bool IsTileOccupied() => currentTileType == TileType.OBSTACLE;

        public void SetTileType(TileType tileType) => currentTileType = tileType;
        public void SetTilePosition(Vector3Int position) => tileGridPosition = position;
    }
}