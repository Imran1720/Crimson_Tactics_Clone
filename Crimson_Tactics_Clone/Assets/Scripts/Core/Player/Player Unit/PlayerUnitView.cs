using CrimsonTactics.Player;
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUnitView : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private bool canUnitMove;

    private Transform target;
    private Quaternion currentRotation;
    private Vector3 velocity;
    private PlayerUnitController playerUnitController;

    private void Start()
    {
        target = null;
        canUnitMove = false;
    }

    private void Update()
    {
        if (!canUnitMove || target == null)
        {
            transform.rotation = currentRotation;
            return;
        }

        CalculateMoveVelocity();
    }

    private void CalculateMoveVelocity()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;
        velocity = direction * moveSpeed;
        rb.velocity = velocity;

        if (IsPlayerUnitAtTarget())
        {
            canUnitMove = false;
            currentRotation = transform.rotation;
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
            velocity = Vector3.zero;
            playerUnitController.InvokePlayerDestinationReached();
        }

        RotateUnit(direction);
    }

    private void RotateUnit(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private bool IsPlayerUnitAtTarget()
    {
        Vector3 targetposition = new Vector3(target.position.x, transform.position.y, target.position.z);
        return Vector3.Distance(targetposition, transform.position) <= 0.1f;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    public void SetPlayerUnitController(PlayerUnitController unitController) => playerUnitController = unitController;
    public void SetTarget(Transform target)
    {
        canUnitMove = true;
        this.target = target;
    }
}
