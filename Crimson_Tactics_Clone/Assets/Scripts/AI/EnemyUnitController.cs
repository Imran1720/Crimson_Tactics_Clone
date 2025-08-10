using CrimsonTactics.Events;
using CrimsonTactics.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.AI
{
    public class EnemyUnitController : UnitController
    {
        //core
        private EventService eventService;
        private EnemyUnitView enemyUnitView;

        public EnemyUnitController(EnemyUnitView enemyUnitView, EventService eventService)
        {
            this.eventService = eventService;
            this.enemyUnitView = enemyUnitView;
            targetCheckPointList = new List<Vector3Int>();

            enemyUnitView.SetEnemyUnitController(this);
            eventService.onPlayerReachedTarget.AddEventListener(OnPlayerUnitReachedTarget);
        }

        public override void Update()
        {
            base.Update();
        }

        protected override void InvokeUnitDestinationReached()
        {
            eventService.onEnemyReachedTarget.InvokeEvent();
        }

        private void OnPlayerUnitReachedTarget(Vector3Int targetPosition)
        {
            finalPosition = targetPosition;
            enemyUnitView.SetRigidbodyDynamic();
            SetTarget(finalPosition);
        }

        protected override void CalculateCheckpoints()
        {
            base.CalculateCheckpoints();
            targetCheckPointList.RemoveAt(targetCheckPointList.Count - 1);
        }

        protected override void SetUnitPosition(Vector3Int targetCheckpoint)
        {
            enemyUnitView.SetUnitPosition(targetCheckpoint);
        }

        protected override Vector3Int GetUnitPosition()
        {
            return enemyUnitView.GetCurrentUnitPosition();
        }
        protected override Vector3 GetUnitWorldPosition()
        {
            return enemyUnitView.GetUnitWorldPosition();
        }

        protected override void SetVelocity()
        {
            enemyUnitView.SetVelocity(CalculateMoveVelocity());
        }

        protected override void RotateCurrentUnit(Quaternion lookRotation)
        {
            enemyUnitView.RotateUnit(lookRotation);
        }

        protected override void StopUnit(Vector3Int targetCheckpoint)
        {
            enemyUnitView.StopUnit(targetCheckpoint);
        }
    }
}