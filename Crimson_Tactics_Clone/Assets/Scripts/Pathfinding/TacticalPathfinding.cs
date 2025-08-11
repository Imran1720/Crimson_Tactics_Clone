using CrimsonTactics.Level;
using System;
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
        //private Stack<Vector3Int> checkpointsList2;

        private int movementVectorX;
        private int movementVectorZ;

        private TileType[,] tileTypesArray;

        public TacticalPathfinding(LevelTileDataSO levelTileDataSO)
        {
            this.levelTileDataSO = levelTileDataSO;

            int row = levelTileDataSO.row;
            int col = levelTileDataSO.col;
            tileTypesArray = new TileType[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tileTypesArray[i, j] = levelTileDataSO.tileDataList[(i * 10) + j].tileType;
                }
            }
        }

        //Setting Pathfinding Data i.e
        //unit start position and target position
        public void SetPathfindingData(Vector3Int startPosition, Vector3Int targetPosition)
        {
            this.startPosition = startPosition;
            this.targetPosition = targetPosition;

            startPosition.y = 0;
            targetPosition.y = 0;
            checkpointsList = new List<Vector3Int>();
            // checkpointsList2 = new Stack<Vector3Int>();

            CalculateMovementVectors();
        }

        // movement vectors are used to calculate checkpoints points a unit appart
        private void CalculateMovementVectors()
        {
            movementVectorX = (startPosition.x > targetPosition.x) ? -1 : 1;
            movementVectorZ = (startPosition.z > targetPosition.z) ? -1 : 1;
        }

        //calculates and returns list of checkpoints
        // Note: this algorithm ignores obstacles
        public List<Vector3Int> GetCheckpoints()
        {
            checkpointsList.Clear();
            movementVector = new Vector3Int(movementVectorX, 0, 0);
            while (startPosition.x != targetPosition.x)
            {
                checkpointsList.Add(startPosition);
                startPosition += movementVector;
            }
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


    }
}