using CrimsonTactics.Events;
using UnityEngine;

namespace CrimsonTactics.Player
{
    // PlayerUntiService is responsible to spawn playerunit at specified spawn location
    public class PlayerUnitService
    {
        private PlayerUnitView unitView;
        private PlayerUnitController unitcontroller;

        public PlayerUnitService(PlayerUnitView unitPrefab, EventService eventService, Vector3 spawnPosition)
        {
            SpawnPlayer(spawnPosition, eventService, unitPrefab);
        }

        private void SpawnPlayer(Vector3 spawnPosition, EventService eventService, PlayerUnitView unitPrefab)
        {
            unitView = GameObject.Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
            unitcontroller = new PlayerUnitController(unitView, eventService);
        }

        public Transform GetUnitTransform() => unitView.transform;
        public int GetPlayerUnitPosistionY() => (int)unitView.transform.position.y;
    }
}
