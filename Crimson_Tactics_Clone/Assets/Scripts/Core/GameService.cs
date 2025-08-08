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

        [SerializeField] private UIService uiService;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private ObstacleTileDataSO tileDataSO;

        [SerializeField] private Transform tileContainer;
        [SerializeField] private TileController obstaclePrefab;

        private void Start()
        {
            eventService = new EventService();
            obstacleService = new ObstacleService(this, tileDataSO);
            InitializeServices();
        }

        private void InitializeServices()
        {
            uiService.InitializeServices(eventService);
            playerController.InitializeService(eventService);
        }

        public EventService GetEventService() => eventService;
        public void CreateObstacleAt(Vector3 position)
        {
            TileController tile = Instantiate(obstaclePrefab, position, Quaternion.identity);
            tile.transform.SetParent(tileContainer, false);
        }
    }
}
