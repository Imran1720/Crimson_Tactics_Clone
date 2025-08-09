using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.AI
{
    public class TacticalPathfinding
    {
        private LevelTileDataSO levelTileDataSO;
        private Vector3Int startPosition;
        private Vector3Int targetPosition;
        private Vector3Int movementVector;

        private List<Vector3Int> checkpointsList;

        private int movementVectorX;
        private int movementVectorZ;

        public TacticalPathfinding(LevelTileDataSO levelTileDataSO)
        {
            this.levelTileDataSO = levelTileDataSO;
        }

        public void SetPathfindingData(Vector3Int startPosition, Vector3Int targetPosition)
        {
            this.startPosition = startPosition;
            this.targetPosition = targetPosition;

            checkpointsList = new List<Vector3Int>();

            CalculateMovementVectors();
        }

        private void CalculateMovementVectors()
        {
            movementVectorX = (startPosition.x > targetPosition.x) ? -1 : 1;
            movementVectorZ = (startPosition.z > targetPosition.z) ? -1 : 1;
        }

        public List<Vector3Int> GetCheckpoints()
        {
            checkpointsList.Clear();
            movementVector = new Vector3Int(movementVectorX, 0, 0);
            while (startPosition.x != targetPosition.x)
            {
                checkpointsList.Add(startPosition);
                startPosition += movementVector;
            }
            checkpointsList.Add(startPosition);
            movementVector = Vector3Int.zero;
            movementVector = new Vector3Int(0, 0, movementVectorZ);
            while (startPosition.z != targetPosition.z)
            {
                checkpointsList.Add(startPosition);
                startPosition += movementVector;
            }
            checkpointsList.Add(startPosition);
            movementVector = Vector3Int.zero;

            return checkpointsList;
        }

        public void GetMoveCheckpoints()
        {
            checkpointsList.Clear();

            while (startPosition.x != targetPosition.x)
            {

            }

        }

        public void CheckPossibleMove()
        {
        }
    }
}