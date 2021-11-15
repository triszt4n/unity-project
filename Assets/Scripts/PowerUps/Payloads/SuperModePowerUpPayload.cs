namespace PowerUps
{
    public class SuperModePowerUpPayload: ITemporaryPowerUp
    {

        private float duration;
        private float moveMultiplier;

        public SuperModePowerUpPayload(float duration, float moveMultiplier)
        {
            this.duration = duration;
            this.moveMultiplier = moveMultiplier;
        }
    

        public void OnAttach(PlayerController player)
        {
            player.hasShield = true;
            player.moveSpeed *= moveMultiplier;
        }

        public void OnDetach(PlayerController player)
        {
            player.hasShield = false;
            player.moveSpeed /= moveMultiplier;
        }

        public float Duration()
        {
            return duration;
        }
    }
}