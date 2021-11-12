using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var player = other.gameObject.GetComponent<PlayerController>();

        if (player.health < player.maxHealth)
        {
           ApplyEffect(player);
        }
    }

    private void ApplyEffect(PlayerController player)
    {
        player.health++;
        player.UpdateHealthUI();
        Destroy(gameObject);
    }
}

