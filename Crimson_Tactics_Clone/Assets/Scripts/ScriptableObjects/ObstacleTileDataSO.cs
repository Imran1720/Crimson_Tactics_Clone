using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    // This SO Stores all Obstacle position to be spawned

    [CreateAssetMenu(fileName = "ObstacleTileDataSO", menuName = "SO/ObstacleTileDataSO")]
    public class ObstacleTileDataSO : ScriptableObject
    {
        public List<Vector3Int> obstaclePositionList;

        public void InitializeData(int ObstacleCount)
        {
            obstaclePositionList = new List<Vector3Int>(ObstacleCount);
        }

        public void AddObstacleTilePosition(Vector3Int position)
        {
            obstaclePositionList.Add(position);
        }
    }
}
