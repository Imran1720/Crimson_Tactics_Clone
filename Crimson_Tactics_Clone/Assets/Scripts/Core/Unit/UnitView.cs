using CrimsonTactics.Unit;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [Header("Core-Data")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator currentAnimator;
    [SerializeField] protected LevelTileDataSO levelTileDataSO;
    [SerializeField] protected UnitData currentUnitData;

    protected Vector3 velocity;

    protected virtual void Update()
    {
        SetAnimation();
    }

    protected virtual void SetAnimation()
    {
        if (currentAnimator == null)
        {
            return;
        }
    }

    protected virtual void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    public void StopUnit(Vector3 position)
    {
        velocity = Vector3.zero;
        rb.velocity = velocity;
        SetDefaultRotation();
        SetRigidbodyKinematic();

    }
    public void RotateUnit(Quaternion lookRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, currentUnitData.rotationSpeed * Time.deltaTime);
    }

    public void SetRigidbodyDynamic() => rb.isKinematic = false;
    public void SetRigidbodyKinematic() => rb.isKinematic = true;

    protected void SetDefaultRotation() => transform.rotation = Quaternion.Euler(0, -90, 0);

    public Vector3Int GetPlayerUnitPosition()
    {
        int x = (int)Mathf.Floor(transform.position.x);
        int y = (int)transform.position.y;
        int z = (int)Mathf.Floor(transform.position.z);

        return new Vector3Int(x, y, z);
    }
    public Vector3 GetPlayerWorldPosition() => transform.position;

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }
}
