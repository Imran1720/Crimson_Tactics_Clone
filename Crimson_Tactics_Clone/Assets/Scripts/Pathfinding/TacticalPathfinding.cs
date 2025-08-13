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

        private List<Vector3Int> nextTilesList;
        private List<Vector3Int> checkpointsList;
        private List<Vector3Int> visitedTilesList;

        private int movementVectorX;
        private int movementVectorZ;

        private TileType[,] tileTypesArray;
        private int[,] pathDetectionArray;

        private bool targetReached = false;

        int row;
        int col;

        public TacticalPathfinding(LevelTileDataSO levelTileDataSO)
        {
            this.levelTileDataSO = levelTileDataSO;

            row = levelTileDataSO.row;
            col = levelTileDataSO.col;
            tileTypesArray = new TileType[row, col];
            pathDetectionArray = new int[row, col];

            nextTilesList = new List<Vector3Int>();
            checkpointsList = new List<Vector3Int>();
            visitedTilesList = new List<Vector3Int>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tileTypesArray[i, j] = levelTileDataSO.tileDataList[(i * 10) + j].GetTileType();
                    pathDetectionArray[i, j] = -1;
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
            nextTilesList.Clear();
            checkpointsList.Clear();
            visitedTilesList.Clear();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    pathDetectionArray[i, j] = -1;
                }
            }
            nextTilesList.Add(startPosition);
            targetReached = false;
            //CalculateMovementVectors();
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

        private int step = -1;

        //This method marks initial cell i.e. player/enemy cell as zero(step = 0) and then finds valid adjacent cells.
        //Then in next iteration they are set to 1 (step =1) and again finds their valid adjacent cells.
        //This keep going on until target position is found 
        // Note: this algorithm detects obstacles and avoid them
        public void CalculatePath()
        {
            step++;
            foreach (Vector3Int tilePosition in nextTilesList)
            {
                pathDetectionArray[tilePosition.x, tilePosition.z] = step;
                visitedTilesList.Add(tilePosition);
                if (targetPosition == tilePosition)
                {
                    targetReached = true;
                    break;
                }
            }

            nextTilesList.Clear();

            //Calculate adjacent tiles
            foreach (Vector3Int tilePosition in visitedTilesList)
            {
                if (targetReached)
                {
                    break;
                }

                int nextX = tilePosition.x + 1;
                int nextZ = tilePosition.z;
                if (nextX >= 0 && nextX < row && tileTypesArray[nextX, nextZ] != TileType.OBSTACLE && pathDetectionArray[nextX, nextZ] == -1)
                {
                    Vector3Int nextPosition = new Vector3Int(nextX, 0, nextZ);
                    if (!nextTilesList.Contains(nextPosition))
                    {
                        nextTilesList.Add(nextPosition);
                    }
                }

                nextX = tilePosition.x - 1;
                nextZ = tilePosition.z;
                if (nextX >= 0 && nextX < row && tileTypesArray[nextX, nextZ] != TileType.OBSTACLE && pathDetectionArray[nextX, nextZ] == -1)
                {
                    Vector3Int nextPosition = new Vector3Int(nextX, 0, nextZ);
                    if (!nextTilesList.Contains(nextPosition))
                    {
                        nextTilesList.Add(nextPosition);
                    }
                }

                nextX = tilePosition.x;
                nextZ = tilePosition.z + 1;
                if (nextZ >= 0 && nextZ < row && tileTypesArray[nextX, nextZ] != TileType.OBSTACLE && pathDetectionArray[nextX, nextZ] == -1)
                {
                    Vector3Int nextPosition = new Vector3Int(nextX, 0, nextZ);
                    if (!nextTilesList.Contains(nextPosition))
                    {
                        nextTilesList.Add(nextPosition);
                    }
                }

                nextX = tilePosition.x;
                nextZ = tilePosition.z - 1;
                if (nextZ >= 0 && nextZ < row && tileTypesArray[nextX, nextZ] != TileType.OBSTACLE && pathDetectionArray[nextX, nextZ] == -1)
                {
                    Vector3Int nextPosition = new Vector3Int(nextX, 0, nextZ);
                    if (!nextTilesList.Contains(nextPosition))
                    {
                        nextTilesList.Add(nextPosition);
                    }
                }
            }
            visitedTilesList.Clear();
            if (!targetReached)
            {
                CalculatePath();
            }
        }

        //This methods finds the valid path from target to starting point and add each position into list
        //Then returns the reversed list as checkpoints
        public List<Vector3Int> GetCheckpointsPath()
        {
            checkpointsList.Clear();
            Vector3Int startTile = targetPosition;
            Vector3Int nextTile;
            while (startTile != startPosition)
            {
                checkpointsList.Add(startTile);
                nextTile = GetNextTile(startTile);
                startTile = nextTile;
            }
            checkpointsList.Reverse();

            return checkpointsList;
        }

        private Vector3Int GetNextTile(Vector3Int startTile)
        {
            int targetTileValue = pathDetectionArray[startTile.x, startTile.z] - 1;

            if (startTile.x - 1 >= 0 && startTile.x - 1 < row && pathDetectionArray[startTile.x - 1, startTile.z] == targetTileValue)
            {
                return new Vector3Int(startTile.x - 1, startTile.y, startTile.z);
            }
            if (startTile.x + 1 >= 0 && startTile.x + 1 < row && pathDetectionArray[startTile.x + 1, startTile.z] == targetTileValue)
            {
                return new Vector3Int(startTile.x + 1, startTile.y, startTile.z);
            }
            if (startTile.z - 1 >= 0 && startTile.z - 1 < col && pathDetectionArray[startTile.x, startTile.z - 1] == targetTileValue)
            {
                return new Vector3Int(startTile.x, startTile.y, startTile.z - 1);
            }
            if (startTile.z + 1 >= 0 && startTile.z + 1 < col && pathDetectionArray[startTile.x, startTile.z + 1] == targetTileValue)
            {
                return new Vector3Int(startTile.x, startTile.y, startTile.z + 1);
            }

            return Vector3Int.zero;
        }

        private bool IsValidTile(int x, int y)
        {
            bool tileInRangeX = x >= 0 && x < levelTileDataSO.row;
            bool tileInRangeY = y >= 0 && y < levelTileDataSO.col;

            TileType currentTile = levelTileDataSO.tileDataList[(x * 10) + y].GetTileType();

            return currentTile == TileType.FREE && tileInRangeX && tileInRangeY;
        }
    }
}