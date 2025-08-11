using UnityEngine;

namespace CrimsonTactics.AI
{
    public interface EnemyAI
    {
        public void EnableMovement();
        public void DisableMovement();
        public void CalculateMovementCheckpoints(Vector3Int checkpoint);
    }
}
