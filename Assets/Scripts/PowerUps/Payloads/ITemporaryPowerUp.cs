namespace PowerUps
{
    public interface ITemporaryPowerUp
    {
        void OnAttach(PlayerController player);
        void OnDetach(PlayerController player);
        float Duration();
    }
}