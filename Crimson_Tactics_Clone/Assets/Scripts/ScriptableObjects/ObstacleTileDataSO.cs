using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    [CreateAssetMenu(fileName = "ObstacleTileDataSO", menuName = "SO/ObstacleTileDataSO")]
    public class ObstacleTileDataSO : ScriptableObject
    {
        public List<Vector3> obstaclePositionList;

        public void InitializeList(int size)
        {
            obstaclePositionList = new List<Vector3>(size);
        }

        public void AddObstacleTilePosition(Vector3 position)
        {
            obstaclePositionList.Add(new Vector3(position.x, position.y, position.z));
        }
    }
}
