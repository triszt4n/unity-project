using UnityEngine;

namespace PowerUps
{
    public class ShieldPowerUpPayload : TemporaryPowerUp
    {
        private GameObject shieldPrefab;
        private GameObject shieldObject;

        public ShieldPowerUpPayload(float duration, GameObject shieldPrefab) : base(duration)
        {
            this.shieldPrefab = shieldPrefab;
        }

        protected override void onAttach(PlayerController player)
        {
            shieldObject = GameObject.Instantiate(shieldPrefab, player.gameObject.transform.position,
                player.gameObject.transform.rotation);
            shieldObject.transform.parent = player.gameObject.transform;
            player.hasShield = true;
        }

        protected override void onDetach(PlayerController player)
        {
            player.hasShield = false;
            GameObject.Destroy(shieldObject);
        }
    }
}