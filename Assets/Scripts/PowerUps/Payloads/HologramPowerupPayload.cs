using UnityEngine;

namespace PowerUps
{
    public class HologramPowerupPayload: ITemporaryPowerUp
    {

        private GameObject hologramPrefab;
        private GameObject hologramObject;
        private float duration;

        public HologramPowerupPayload(GameObject hologramPrefab, float duration)
        {
            this.hologramPrefab = hologramPrefab;
            this.duration = duration;
        }

        public void OnAttach(PlayerController player)
        {
            var gameObject = player.gameObject;
            hologramObject = GameObject.Instantiate(hologramPrefab, gameObject.transform.position, hologramPrefab.transform.rotation);

            var hologramController = hologramObject.GetComponent<HologramController>();
            hologramController.player = gameObject;
        }

        public void OnDetach(PlayerController player)
        {
            GameObject.Destroy(hologramObject);
        }

        public float Duration()
        {
            return duration;
        }
    }
}