using CrimsonTactics.Core;
using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    public class ObstacleService
    {
        private GameService gameService;
        private ObstacleTileDataSO obstacleTileDataSO;

        public ObstacleService(GameService gameService, ObstacleTileDataSO obstacleTileDataSO)
        {
            this.gameService = gameService;
            this.obstacleTileDataSO = obstacleTileDataSO;

            GenerateObstacle();
        }

        // Spawns obstacle at locations present in obstacleTileDataSO using GameService
        private void GenerateObstacle()
        {
            List<Vector3Int> obstaclePositionList = obstacleTileDataSO.obstaclePositionList;
            foreach (Vector3 position in obstaclePositionList)
            {
                gameService.CreateObstacleAt(position);
            }
        }
    }
}
