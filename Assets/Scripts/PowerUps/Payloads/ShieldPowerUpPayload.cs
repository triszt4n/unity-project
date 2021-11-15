namespace PowerUps
{
    public class ShieldPowerUpPayload : ITemporaryPowerUp
    {

        private float duration;

        public ShieldPowerUpPayload(float duration)
        {
            this.duration = duration;
        }

        public void OnAttach(PlayerController player)
        {
            player.hasShield = true;
        }

        public void OnDetach(PlayerController player)
        {
            player.hasShield = false;
        }
        

        public float Duration()
        {
            return duration;
        }
        
    }
}