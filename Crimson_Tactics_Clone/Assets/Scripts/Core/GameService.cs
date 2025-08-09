using CrimsonTactics.Events;
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

        [Header("Dependencies")]
        [SerializeField] private UIService uiService;
        [SerializeField] private PlayerInputController playerController;

        [Header("Tile-Data")]
        [SerializeField] private Transform tileContainer;
        [SerializeField] private TileController obstaclePrefab;
        [SerializeField] private ObstacleTileDataSO tileDataSO;

        [Header("Player-Unit-Data")]
        [SerializeField] private PlayerUnitView playerUnitView;
        [SerializeField] private Vector3Int minSpawnPosition;
        [SerializeField] private Vector3Int maxSpawnPosition;

        private void Start()
        {
            eventService = new EventService();
            obstacleService = new ObstacleService(this, tileDataSO);
            Vector3 playerSpawnPosition = GetSpawnPosiition();
            playerUnitService = new PlayerUnitService(playerUnitView, eventService, playerSpawnPosition);

            InitializeData();
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

            SetTilePositionArray();
        }

        private void SetTilePositionArray()
        {
            int rows = tileDataSO.tilesInRow;
            int columns = tileDataSO.tilesInColumn;
        }

        public EventService GetEventService() => eventService;
        public void CreateObstacleAt(Vector3 position)
        {
            TileController tile = Instantiate(obstaclePrefab, position, Quaternion.identity);
            tile.transform.SetParent(tileContainer, false);
        }
    }
}
