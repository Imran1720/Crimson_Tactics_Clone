using CrimsonTactics.Events;
using UnityEngine;

namespace CrimsonTactics.Player
{
    public class PlayerUnitService
    {
        private PlayerUnitController unitcontroller;
        private PlayerUnitView unitPrefab;
        private PlayerUnitView unitView;

        public PlayerUnitService(PlayerUnitView unitPrefab, EventService eventService, Vector3 spawnPosition)
        {
            this.unitPrefab = unitPrefab;

            SpawnPlayer(spawnPosition, eventService);
        }

        private void SpawnPlayer(Vector3 spawnPosition, EventService eventService)
        {

            unitView = GameObject.Instantiate(unitPrefab);
            unitView.transform.position = spawnPosition;
            unitcontroller = new PlayerUnitController(unitView, eventService);
        }
    }
}
