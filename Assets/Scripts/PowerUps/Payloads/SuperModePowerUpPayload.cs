using System.Linq;
using UnityEngine;

namespace PowerUps
{
    public class SuperModePowerUpPayload : TemporaryPowerUp
    {
        private float moveMultiplier;
        private Color originalColor;
        private SpriteRenderer flameRenderer;
        private Transform flameContainer;

        public SuperModePowerUpPayload(float duration, float moveMultiplier) : base(duration)
        {
            this.moveMultiplier = moveMultiplier;
        }


        protected override void onAttach(PlayerController player)
        {
            flameRenderer = player.gameObject
                .GetComponentsInChildren<SpriteRenderer>()
                .FirstOrDefault(r => r.gameObject.CompareTag("Flame"));

            originalColor = flameRenderer.color;
            flameRenderer.color = new Color(255, 0, 0);
            flameContainer = flameRenderer.gameObject.transform.parent;
            if (flameContainer != null)
                flameContainer.localScale = new Vector3(1.5f, 1.5f, 1f);
            player.moveSpeed *= moveMultiplier;
        }

        protected override void onDetach(PlayerController player)
        {
            if (flameContainer != null)
                flameContainer.localScale = new Vector3(1f, 1f, 1f);
            flameRenderer.color = originalColor;
            player.moveSpeed /= moveMultiplier;
        }
    }
}