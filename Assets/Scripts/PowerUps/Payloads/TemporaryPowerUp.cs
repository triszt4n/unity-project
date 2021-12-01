namespace PowerUps
{
    public abstract class TemporaryPowerUp
    {
        private bool attached = false;
        private float duration;

        public float Duration
        {
            get { return duration; }
        }

        public TemporaryPowerUp(float duration)
        {
            this.duration = duration;
        }

        public void OnAttach(PlayerController player)
        {
            if (attached) return;
            attached = true;
            onAttach(player);
        }

        public void OnDetach(PlayerController player)
        {
            if (!attached) return;
            attached = false;
            onDetach(player);
        }


        protected abstract void onAttach(PlayerController player);
        protected abstract void onDetach(PlayerController player);
    }
}