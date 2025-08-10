namespace CrimsonTactics.AI
{
    public class EnemyUnitView : UnitView
    {
        private EnemyUnitController enemyUnitController;

        protected override void Update()
        {
            base.Update();
            enemyUnitController.Update();
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
    }
}
