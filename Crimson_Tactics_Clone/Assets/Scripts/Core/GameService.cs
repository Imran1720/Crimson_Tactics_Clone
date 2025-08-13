using Cinemachine;
using CrimsonTactics.AI;
using CrimsonTactics.Events;
using CrimsonTactics.Level;
using CrimsonTactics.Player;
using CrimsonTactics.Tile;
using CrimsonTactics.UI;
using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Core
{
    public class GameService : MonoBehaviour
    {
        private EventService eventService;
        private ObstacleService obstacleService;
        private PlayerUnitService playerUnitService;
        private EnemyService enemyService;

        [Header("Dependencies")]
        [SerializeField] private UIService uiService;
        [SerializeField] private PlayerInputController playerController;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        [Header("Tile-Data")]
        [SerializeField] private Transform tileContainer;
        [SerializeField] private TileController obstaclePrefab;
        [SerializeField] private LevelTileDataSO levelTileDataSO;
        [SerializeField] private ObstacleTileDataSO obstacleTileDataSO;

        [Header("Player-Unit-Data")]
        [SerializeField] private PlayerUnitView playerUnitView;

        [Header("Enemy-Unit-Data")]
        [SerializeField] private EnemyUnitView enemyUnitPrefab;

        private void Start()
        {
            if (transform.childCount > 0)
            {
                CreateServices();
                SetCameraTarget();

                InitializeData();
            }
        }

        private void SetCameraTarget()
        {
            cinemachineVirtualCamera.Follow = playerUnitService.GetUnitTransform();
            cinemachineVirtualCamera.LookAt = playerUnitService.GetUnitTransform();
        }

        private Vector3 GetSpawnPosiition()
        {
            Vector3 postion;
            int spawnPositionX = Random.Range(0, levelTileDataSO.row);
            int spawnPositionz = Random.Range(0, levelTileDataSO.col);
            postion = new Vector3(spawnPositionX, 0, spawnPositionz);
            if (GetTileType(spawnPositionX, spawnPositionz) == TileType.OBSTACLE)
            {
                postion = GetSpawnPosiition();
            }
            return postion;
        }

        private TileType GetTileType(int spawnPositionX, int spawnPositionz)
        {
            List<TileStorageData> tileStorageData = levelTileDataSO.tileDataList;

            return tileStorageData[(spawnPositionX * 10) + spawnPositionz].GetTileType();
        }

        private void InitializeData()
        {
            uiService.InitializeData(eventService);
            playerController.InitializeData(eventService, levelTileDataSO);
        }

        private void CreateServices()
        {
            tileContainer = transform.GetChild(0);

            eventService = new EventService();
            Vector3 playerSpawnPosition = GetSpawnPosiition();
            obstacleService = new ObstacleService(this, obstacleTileDataSO);
            playerUnitService = new PlayerUnitService(playerUnitView, eventService, playerSpawnPosition);
            Vector3 enemySpawnPosition = GetEnemySpawnPosition(playerSpawnPosition);
            enemyService = new EnemyService(enemyUnitPrefab, eventService, enemySpawnPosition);
        }

        private Vector3 GetEnemySpawnPosition(Vector3 playerPosition)
        {
            Vector3 spawnPosition = GetSpawnPosiition();
            if (spawnPosition == playerPosition)
            {
                spawnPosition = GetEnemySpawnPosition(playerPosition);
            }

            return spawnPosition;
        }

        public EventService GetEventService() => eventService;

        //Method to spawn Obstacle At specified position
        public void CreateObstacleAt(Vector3 position)
        {
            TileController tile = Instantiate(obstaclePrefab, position, Quaternion.identity);
            tile.transform.SetParent(tileContainer, true);
        }
    }
}
