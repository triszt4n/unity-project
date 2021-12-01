namespace PowerUps
{
    public class ShieldPowerUpPayload : TemporaryPowerUp
    {
        public ShieldPowerUpPayload(float duration) : base(duration)
        {
        }

        protected override void onAttach(PlayerController player)
        {
            player.hasShield = true;
        }

        protected override void onDetach(PlayerController player)
        {
            player.hasShield = false;
        }
    }
}