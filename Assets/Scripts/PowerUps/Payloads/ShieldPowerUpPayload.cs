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
            player.AddShield();
        }

        public void OnDetach(PlayerController player)
        {
            player.RemoveShield();
        }
        

        public float Duration()
        {
            return duration;
        }
        
    }
}