using Cinemachine;
using CrimsonTactics.AI;
using CrimsonTactics.Events;
using CrimsonTactics.Player;
using CrimsonTactics.Tile;
using CrimsonTactics.UI;
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
        [SerializeField] private TileController obstaclePrefab;
        [SerializeField] private ObstacleTileDataSO tileDataSO;
        [SerializeField] private Transform tileContainer;

        [Header("Player-Unit-Data")]
        [SerializeField] private Vector3Int minSpawnPosition;
        [SerializeField] private Vector3Int maxSpawnPosition;
        [SerializeField] private PlayerUnitView playerUnitView;

        [Header("Enemy-Unit-Data")]
        [SerializeField] private EnemyUnitView enemyUnitPrefab;

        private void Start()
        {
            CreateServices();
            SetCameraTarget();

            InitializeData();
        }

        private void SetCameraTarget()
        {
            cinemachineVirtualCamera.Follow = playerUnitService.GetUnitTransform();
            cinemachineVirtualCamera.LookAt = playerUnitService.GetUnitTransform();
        }

        private Vector3 GetSpawnPosiition()
        {
            int spawnPositionX = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
            int spawnPositionz = Random.Range(minSpawnPosition.z, maxSpawnPosition.z);
            return new Vector3(spawnPositionX, minSpawnPosition.y, spawnPositionz);
        }

        private void InitializeData()
        {
            uiService.InitializeData(eventService);
            playerController.InitializeData(eventService);
        }

        private void CreateServices()
        {
            if (transform.childCount > 0)
            {
                tileContainer = transform.GetChild(0);

                eventService = new EventService();
                Vector3 playerSpawnPosition = GetSpawnPosiition();
                obstacleService = new ObstacleService(this, tileDataSO);
                playerUnitService = new PlayerUnitService(playerUnitView, eventService, playerSpawnPosition);
                Vector3 enemySpawnPosition = GetSpawnPosiition();
                enemyService = new EnemyService(enemyUnitPrefab, eventService, enemySpawnPosition);
            }
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
            tile.transform.SetParent(tileContainer, false);
        }
    }
}
