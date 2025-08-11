using CrimsonTactics.Core;
using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Tile
{
    public class ObstacleService
    {
        private ObstacleTileDataSO obstacleTileDataSO;

        private GameService gameService;
        public ObstacleService(GameService gameService, ObstacleTileDataSO obstacleTileDataSO)
        {
            this.gameService = gameService;
            this.obstacleTileDataSO = obstacleTileDataSO;

            GenerateObstacle();
        }

        private void GenerateObstacle()
        {
            List<Vector3> obstaclePositionList = obstacleTileDataSO.obstaclePositionList;
            foreach (Vector3 position in obstaclePositionList)
            {
                gameService.CreateObstacleAt(position);
            }
        }
    }
}
