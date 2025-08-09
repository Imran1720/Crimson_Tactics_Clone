using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    [CreateAssetMenu(fileName = "ObstacleTileDataSO", menuName = "SO/ObstacleTileDataSO")]
    public class ObstacleTileDataSO : ScriptableObject
    {
        public int tilesInRow;
        public int tilesInColumn;

        public List<Vector3> obstaclePositionList;
        public List<Vector3Int> tilePositionList;

        public void InitializeData(int sizeX, int sizeY, int ObstacleCount)
        {
            tilesInRow = sizeX;
            tilesInColumn = sizeY;
            obstaclePositionList = new List<Vector3>(ObstacleCount);
            tilePositionList = new List<Vector3Int>();
        }


        public void AddObstacleTilePosition(Vector3 position)
        {
            obstaclePositionList.Add(new Vector3(position.x, position.y, position.z));
        }

        public void AddTilePosition(Vector3Int position)
        {
            tilePositionList.Add(position);
        }
    }
}
