namespace PowerUps
{
    public class SuperModePowerUpPayload : TemporaryPowerUp
    {
        private float moveMultiplier;

        public SuperModePowerUpPayload(float duration, float moveMultiplier) : base(duration)
        {
            this.moveMultiplier = moveMultiplier;
        }


        protected override void onAttach(PlayerController player)
        {
            player.moveSpeed *= moveMultiplier;
        }

        protected override void onDetach(PlayerController player)
        {
            player.moveSpeed /= moveMultiplier;
        }
    }
}