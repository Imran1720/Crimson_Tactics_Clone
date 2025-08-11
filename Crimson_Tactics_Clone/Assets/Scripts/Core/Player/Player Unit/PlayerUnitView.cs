namespace CrimsonTactics.Player
{
    public class PlayerUnitView : UnitView
    {

        private PlayerUnitController playerUnitController;

        protected override void Update()
        {
            base.Update();
            playerUnitController.Update();
        }

        protected override void SetAnimation()
        {
            currentAnimator.SetFloat("Velocity", rb.velocity.magnitude);
        }

        public void SetPlayerUnitController(PlayerUnitController unitController)
        {
            playerUnitController = unitController;

            playerUnitController.InitializeData(currentUnitData, GetCurrentUnitPosition());
        }
    }
}