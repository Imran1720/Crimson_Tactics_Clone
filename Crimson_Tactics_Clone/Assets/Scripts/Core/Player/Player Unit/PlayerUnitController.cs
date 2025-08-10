using CrimsonTactics.Events;
using CrimsonTactics.Tile;
using CrimsonTactics.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Player
{
    public class PlayerUnitController : UnitController
    {
        //core
        private EventService eventService;
        private PlayerUnitView playerUnitView;

        public PlayerUnitController(PlayerUnitView playerUnitView, EventService eventService) : base()
        {
            this.playerUnitView = playerUnitView;
            this.eventService = eventService;

            targetCheckPointList = new List<Vector3Int>();
            playerUnitView.SetPlayerUnitController(this);
            eventService.onTargetTileSelected.AddEventListener(OnTragetTileSelected);
        }

        public override void Update()
        {
            base.Update();
        }

        // Event listener to get the selected grid poisition on player seleting a grid
        private void OnTragetTileSelected(TileController tileController)
        {
            finalPosition = tileController.GetTileGridPosition();
            playerUnitView.SetRigidbodyDynamic();
            SetTarget(finalPosition);
        }

        // Event to notify that player is reached the target to enable Input 
        protected override void InvokeUnitDestinationReached()
        {
            eventService.onPlayerReachedTarget.InvokeEvent(targetCheckpoint);
        }

        protected override bool IsPlayer()
        {
            return true;
        }

        protected override void SetUnitPosition(Vector3Int targetCheckpoint)
        {
            playerUnitView.SetUnitPosition(targetCheckpoint);
        }
        protected override Vector3Int GetUnitPosition() => playerUnitView.GetCurrentUnitPosition();
        protected override Vector3 GetUnitWorldPosition() => playerUnitView.GetUnitWorldPosition();
        protected override void SetVelocity() => playerUnitView.SetVelocity(CalculateMoveVelocity());
        protected override void StopUnit(Vector3Int targetCheckpoint) => playerUnitView.StopUnit(targetCheckpoint);
        protected override void RotateCurrentUnit(Quaternion lookRotation) => playerUnitView.RotateUnit(lookRotation);
    }
}
