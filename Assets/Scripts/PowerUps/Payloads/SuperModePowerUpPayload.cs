using System.Linq;
using UnityEngine;

namespace PowerUps
{
    public class SuperModePowerUpPayload : TemporaryPowerUp
    {
        private float moveMultiplier;
        private Color originalColor;
        private SpriteRenderer flameRenderer;

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
            flameRenderer.color = new Color(255,0,0);
            player.moveSpeed *= moveMultiplier;
        }

        protected override void onDetach(PlayerController player)
        {
            flameRenderer.color = originalColor;
            player.moveSpeed /= moveMultiplier;
        }
    }
}