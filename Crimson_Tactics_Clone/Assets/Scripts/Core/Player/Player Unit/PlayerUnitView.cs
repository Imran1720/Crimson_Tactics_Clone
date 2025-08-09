using CrimsonTactics.AI;
using CrimsonTactics.Player;
using CrimsonTactics.Tile;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitView : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private List<Vector3Int> targetCheckPointList;
    [SerializeField] private ObstacleTileDataSO obstacleTileDataSO;

    private bool canUnitMove;

    private Transform target;
    private Vector3Int targetCheckpoint;
    private Vector3Int finalPosition;
    private Quaternion currentRotation;
    private Vector3 velocity;
    private PlayerUnitController playerUnitController;
    private TacticalPathfinding pathfinding;

    private void Start()
    {
        target = null;
        canUnitMove = false;
        pathfinding = new TacticalPathfinding(obstacleTileDataSO);
    }

    private void Update()
    {
        if (targetCheckPointList.Count <= 0 && IsPlayerUnitAtTarget())
        {
            StopUnit();
            return;
        }

        SetTargetCheckpoint();
        CalculateMoveVelocity();
    }

    private void StopUnit()
    {
        velocity = Vector3.zero;
        rb.velocity = Vector3Int.zero;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        transform.position = finalPosition;
        playerUnitController.InvokePlayerDestinationReached();
    }

    private void SetTargetCheckpoint()
    {
        if (IsPlayerUnitAtTarget())
        {
            targetCheckpoint = targetCheckPointList[0];
            targetCheckPointList.RemoveAt(0);
        }
    }

    private void CalculateMoveVelocity()
    {
        Vector3 direction = (targetCheckpoint - transform.position).normalized;
        direction.y = transform.position.y;

        velocity = direction * moveSpeed;
        RotateUnit(direction);
    }

    private void RotateUnit(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private bool IsPlayerUnitAtTarget()
    {
        return Vector3.Distance(targetCheckpoint, transform.position) <= 0.1f;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    public void SetPlayerUnitController(PlayerUnitController unitController) => playerUnitController = unitController;
    public void SetTarget(Vector2Int targetPosition)
    {
        Vector3Int targetGridPosition = new Vector3Int(targetPosition.x, (int)transform.position.y, targetPosition.y);
        finalPosition = targetGridPosition;
        Vector3Int currentPosition = GetPlayerUnitPosition();
        pathfinding.SetPathfindingData(currentPosition, targetGridPosition);

        targetCheckPointList.Clear();
        targetCheckPointList = pathfinding.GetCheckpoints();
    }

    public Vector3Int GetPlayerUnitPosition()
    {
        int x = (int)Mathf.Floor(transform.position.x);
        int y = (int)transform.position.y;
        int z = (int)Mathf.Floor(transform.position.z);

        return new Vector3Int(x, y, z);
    }
}
