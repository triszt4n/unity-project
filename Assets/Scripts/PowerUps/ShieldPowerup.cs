using System;
using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class ShieldPowerup : MonoBehaviour
{
    public float shieldDuration = 5f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;

        var powerUpController = other.gameObject.GetComponent<TemporaryPowerUpController>();

        if (powerUpController.AddPowerUp(new ShieldPowerUpPayload(shieldDuration)))
        {
            Destroy(gameObject);
        }

    }
}
