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

        public bool Compatible(ITemporaryPowerUp powerUp)
        {
            return (!(powerUp is ShieldPowerUpPayload));
        }

        public float Duration()
        {
            return duration;
        }

        public int IconIndex()
        {
            return 1;
        }
    }
}