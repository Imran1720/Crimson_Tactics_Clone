using CrimsonTactics.AI;
using CrimsonTactics.Events;
using CrimsonTactics.Tile;
using CrimsonTactics.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Player
{
    public class PlayerUnitController
    {
        //core
        private EventService eventService;
        private PlayerUnitView playerUnitView;
        private LevelTileDataSO levelTileDataSO;

        private float moveSpeed;
        private float rotationSpeed;

        // transform Data
        private Quaternion currentRotation;

        private Vector3Int finalPosition;
        private Vector3Int targetCheckpoint;

        // Dependencies
        private TacticalPathfinding pathfinding;
        private List<Vector3Int> targetCheckPointList;

        public PlayerUnitController(PlayerUnitView playerUnitView, EventService eventService)
        {
            this.playerUnitView = playerUnitView;
            this.eventService = eventService;

            targetCheckPointList = new List<Vector3Int>();
            playerUnitView.SetPlayerUnitController(this);
            eventService.onTargetTileSelected.AddEventListener(OnTragetTileSelected);
        }

        public void InitializeData(UnitData currentUnitData, Vector3Int currentUnitPosition)
        {
            moveSpeed = currentUnitData.moveSpeed;
            rotationSpeed = currentUnitData.rotationSpeed;
            levelTileDataSO = currentUnitData.levelTileDataSO;

            finalPosition = currentUnitPosition;
            targetCheckpoint = currentUnitPosition;
            pathfinding = new TacticalPathfinding(levelTileDataSO);
        }

        public void Update()
        {
            if (UnitAtFinalTarget())
            {
                playerUnitView.StopUnit(targetCheckpoint);
                InvokePlayerDestinationReached();
                return;
            }

            SetTargetCheckpoint();
            playerUnitView.SetVelocity(CalculateMoveVelocity());
            RotateUnit();
        }

        // Updating checkpoints based on the list   
        private void SetTargetCheckpoint()
        {
            if (IsUnitAtTargetCheckpoint())
            {
                targetCheckpoint = targetCheckPointList[0];
                targetCheckPointList.RemoveAt(0);
            }
        }

        //Setting target and data for calculating the checkpoints for unit to follow
        public void SetTarget(Vector3Int targetPosition)
        {
            Vector3Int currentPosition = playerUnitView.GetPlayerUnitPosition();
            pathfinding.SetPathfindingData(currentPosition, targetPosition);

            CalculateCheckpoints();
        }

        //calculating checkpoints
        private void CalculateCheckpoints()
        {
            targetCheckPointList.Clear();
            targetCheckPointList = pathfinding.GetCheckpoints();
        }

        // Event listener to get the selected grid poisition on player seleting a grid
        private void OnTragetTileSelected(TileController tileController)
        {
            finalPosition = tileController.GetTileGridPosition();
            SetTarget(finalPosition);
        }

        // Event to notify that player is reached the target to enable Input 
        public void InvokePlayerDestinationReached()
        {
            eventService.onPlayerReachedTarget.InvokeEvent();
        }

        //Calculating direction and based on it calculating Velocity amd returning
        // checkpoint : target, unitTransform : current unit transform
        public Vector3 CalculateMoveVelocity()
        {
            Vector3 unitTransform = playerUnitView.GetPlayerWorldPosition();
            Vector3 direction = (targetCheckpoint - unitTransform).normalized;
            direction.y = unitTransform.y;

            return direction * moveSpeed;
        }

        //Calculating player Rotation based on target checkpoints
        public Quaternion CalculateLookRotation(Vector3Int checkpoint, Vector3 unitTransform, float rotationSpeed)
        {
            Vector3 direction = (checkpoint - unitTransform).normalized;
            direction.y = unitTransform.y;

            return Quaternion.LookRotation(direction);
        }

        private void RotateUnit()
        {
            Vector3 position = playerUnitView.GetPlayerWorldPosition();
            Quaternion lookRotation = CalculateLookRotation(targetCheckpoint, position, rotationSpeed);
            playerUnitView.RotateUnit(lookRotation);
        }

        private bool UnitAtFinalTarget() => IsUnitAtTargetCheckpoint() && targetCheckPointList.Count <= 0;

        private bool IsUnitAtTargetCheckpoint() => Vector3.Distance(targetCheckpoint, playerUnitView.GetPlayerWorldPosition()) <= 0.1f;
    }
}
