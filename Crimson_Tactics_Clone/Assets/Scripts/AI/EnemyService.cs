using CrimsonTactics.Events;
using UnityEngine;

namespace CrimsonTactics.AI
{
    public class EnemyService
    {
        private EnemyUnitController enemyUnitController;
        private EnemyUnitView enemyUnitView;

        public EnemyService(EnemyUnitView enemyPrefab, EventService eventService, Vector3 spawnPosition)
        {
            enemyUnitView = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyUnitController = new EnemyUnitController(enemyUnitView, eventService);
        }

        public Transform GetUnitPosition() => enemyUnitView.transform;
    }
}