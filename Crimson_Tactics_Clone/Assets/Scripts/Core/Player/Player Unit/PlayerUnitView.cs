using CrimsonTactics.AI;
using CrimsonTactics.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace CrimsonTactics.Player
{
    public class PlayerUnitView : MonoBehaviour
    {
        [Header("Core-Data")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Animator currentAnimator;
        [SerializeField] private LevelTileDataSO levelTileDataSO;
        [SerializeField] private UnitData currentUnitData;

        [Header("Locomotion-Data")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;

        //// transform Data
        //private Transform target;
        //private Quaternion currentRotation;

        private Vector3 velocity;
        //private Vector3Int finalPosition;
        //private Vector3Int targetCheckpoint;

        //// Dependencies
        //private TacticalPathfinding pathfinding;
        //private List<Vector3Int> targetCheckPointList;
        private PlayerUnitController playerUnitController;

        private void Update()
        {
            playerUnitController.Update();
            SetAnimation();
        }

        private void FixedUpdate()
        {
            rb.velocity = velocity;
        }

        public void StopUnit(Vector3 position)
        {
            SetDefaultRotation();
            velocity = Vector3.zero;
            transform.position = position;
        }

        private void SetDefaultRotation() => transform.rotation = Quaternion.Euler(0, -90, 0);

        //private void SetTargetCheckpoint()
        //{
        //    if (IsPlayerUnitAtTarget())
        //    {
        //        targetCheckpoint = targetCheckPointList[0];
        //        targetCheckPointList.RemoveAt(0);
        //    }
        //}

        public void RotateUnit(Quaternion lookRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        //private bool IsPlayerUnitAtTarget() => Vector3.Distance(targetCheckpoint, transform.position) <= 0.1f;

        //public void SetTarget(Vector3Int targetPosition)
        //{
        //    finalPosition = targetPosition;
        //    Vector3Int currentPosition = GetPlayerUnitPosition();
        //    pathfinding.SetPathfindingData(currentPosition, targetPosition);

        //    CalculateCheckpoints();
        //}

        //private void CalculateCheckpoints()
        //{
        //    targetCheckPointList.Clear();
        //    targetCheckPointList = pathfinding.GetCheckpoints();
        //}

        // Removing float Values from player units transform
        public Vector3Int GetPlayerUnitPosition()
        {
            int x = (int)Mathf.Floor(transform.position.x);
            int y = (int)transform.position.y;
            int z = (int)Mathf.Floor(transform.position.z);

            return new Vector3Int(x, y, z);
        }
        public Vector3 GetPlayerWorldPosition() => transform.position;

        private void SetAnimation()
        {
            currentAnimator.SetFloat("Velocity", rb.velocity.magnitude);
        }

        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }

        public void SetPlayerUnitController(PlayerUnitController unitController)
        {
            playerUnitController = unitController;

            playerUnitController.InitializeData(currentUnitData, GetPlayerUnitPosition());
        }
    }
}