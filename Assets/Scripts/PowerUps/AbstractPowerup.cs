using System;
using UnityEngine;

namespace PowerUps
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class AbstractPowerup : MonoBehaviour
    {
        public AudioClip pickupSound;

        [Range(0.0f, 1.0f)]
        public float pickupSoundVolume = 1.0f;
        
        //returns if application is successful
        protected abstract bool TryApplyEffect(GameObject playerObject);
        private void OnTriggerEnter2D(Collider2D other)
        {
            //check if collider is player
            if (!other.gameObject.CompareTag("Player")) return;
            
            //Try applying the effect of the power, if unsuccessful nothing happens.
            if (!TryApplyEffect(other.gameObject)) return;
            
            // if there is a playable audio play it
            var powerUpPlayer = other.gameObject.GetComponent<AudioSource>();
            if (powerUpPlayer != null && pickupSound != null) powerUpPlayer.PlayOneShot(pickupSound,pickupSoundVolume);
            
            Destroy(gameObject);
        }
    }
}