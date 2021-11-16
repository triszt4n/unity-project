using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SuperModePowerUp : MonoBehaviour
{
    public float superModeDuration = 5f;
    public float moveMultiplier = 1.5f;

    public Sprite icon;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;

        var powerUpController = other.gameObject.GetComponent<TemporaryPowerUpController>();

        if (powerUpController.AddPowerUp(new SuperModePowerUpPayload(superModeDuration,moveMultiplier),icon))
        {
            Destroy(gameObject);
        }

    }
}
