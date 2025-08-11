using CrimsonTactics.AI;
using CrimsonTactics.Player;
using CrimsonTactics.Tile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Unit
{
    public class UnitController
    {
        //core
        protected LevelTileDataSO levelTileDataSO;

        protected float moveSpeed;
        protected float rotationSpeed;

        // transform Data
        protected Quaternion currentRotation;

        protected Vector3Int finalPosition;
        protected Vector3Int targetCheckpoint;

        // Dependencies
        protected TacticalPathfinding pathfinding;
        protected List<Vector3Int> targetCheckPointList;

        public UnitController()
        {
            targetCheckPointList = new List<Vector3Int>();
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

        public virtual void Update()
        {
            if (UnitAtFinalTarget())
            {
                StopUnit(targetCheckpoint);
                return;
            }

            SetTargetCheckpoint();
            SetVelocity();
            RotateUnit();
        }

        // Updating checkpoints based on the list   
        protected void SetTargetCheckpoint()
        {
            if (IsUnitAtTargetCheckpoint())
            {
                targetCheckpoint = targetCheckPointList[0];
                targetCheckPointList.RemoveAt(0);
                if (targetCheckPointList.Count == 0)
                {
                    InvokeUnitDestinationReached();
                }
            }
        }

        protected virtual void SetUnitPosition(Vector3Int targetCheckpoint) { }

        //Setting target and data for calculating the checkpoints for unit to follow
        public void SetTarget(Vector3Int targetPosition)
        {
            Vector3Int currentPosition = GetUnitPosition();
            pathfinding.SetPathfindingData(currentPosition, targetPosition);

            CalculateCheckpoints();
        }

        //calculating checkpoints
        protected virtual void CalculateCheckpoints()
        {
            targetCheckPointList.Clear();
            targetCheckPointList = pathfinding.GetCheckpoints();
        }

        //Calculating direction and based on it calculating Velocity amd returning
        // checkpoint : target, unitTransform : current unit transform
        public Vector3 CalculateMoveVelocity()
        {
            Vector3 unitTransform = GetUnitWorldPosition();
            Vector3 direction = (targetCheckpoint - unitTransform).normalized;
            direction.y = 0;
            return direction * moveSpeed;
        }

        //Calculating player Rotation based on target checkpoints
        public Quaternion CalculateLookRotation(Vector3Int checkpoint, Vector3 unitTransform, float rotationSpeed)
        {
            Vector3 direction = (checkpoint - unitTransform).normalized;
            direction.y = 0;

            return Quaternion.LookRotation(direction);
        }

        protected void RotateUnit()
        {
            Vector3 position = GetUnitWorldPosition();
            Quaternion lookRotation = CalculateLookRotation(targetCheckpoint, position, rotationSpeed);
            RotateCurrentUnit(lookRotation);
        }

        protected virtual bool IsPlayer() => false;
        protected virtual void SetVelocity() { }
        protected virtual void InvokeUnitDestinationReached() { }
        protected virtual void StopUnit(Vector3Int targetCheckpoint) { }
        protected virtual void RotateCurrentUnit(Quaternion lookRotation) { }
        protected virtual Vector3Int GetUnitPosition() => Vector3Int.zero;
        protected virtual Vector3 GetUnitWorldPosition() => Vector3.zero;
        public Vector3Int GetTargetPosition() => finalPosition;
        protected bool UnitAtFinalTarget() => IsUnitAtTargetCheckpoint() && targetCheckPointList.Count <= 0;
        protected bool IsUnitAtTargetCheckpoint()
        {
            return Vector3.Distance(targetCheckpoint, GetUnitWorldPosition()) <= 0.1f;
        }
    }
}
