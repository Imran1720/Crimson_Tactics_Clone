namespace CrimsonTactics.AI
{
    public class EnemyUnitView : UnitView
    {
        private EnemyUnitController enemyUnitController;
        private bool canMove = false;

        protected override void Update()
        {
            if (canMove)
            {
                base.Update();
                enemyUnitController.Update();
            }
        }

        protected override void SetAnimation()
        {
            currentAnimator.SetFloat("Velocity", rb.velocity.magnitude);
        }

        public void SetEnemyUnitController(EnemyUnitController enemyUnitController)
        {
            this.enemyUnitController = enemyUnitController;

            enemyUnitController.InitializeData(currentUnitData, GetCurrentUnitPosition());
        }

        public void EnableMovement() => canMove = true;
        public void DisableMovement()
        {
            currentAnimator.SetFloat("Velocity", 0);
            canMove = false;
        }
    }
}
