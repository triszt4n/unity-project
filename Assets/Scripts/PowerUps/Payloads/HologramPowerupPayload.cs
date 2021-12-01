using UnityEngine;

namespace PowerUps
{
    public class HologramPowerupPayload : TemporaryPowerUp
    {
        private GameObject hologramPrefab;
        private GameObject hologramObject;

        public HologramPowerupPayload(float duration, GameObject hologramPrefab) : base(duration)
        {
            this.hologramPrefab = hologramPrefab;
        }

        protected override void onAttach(PlayerController player)
        {
            var gameObject = player.gameObject;
            hologramObject = GameObject.Instantiate(hologramPrefab, gameObject.transform.position,
                hologramPrefab.transform.rotation);

            var hologramController = hologramObject.GetComponent<HologramController>();
            hologramController.player = gameObject;
        }

        protected override void onDetach(PlayerController player)
        {
            GameObject.Destroy(hologramObject);
        }
    }
}