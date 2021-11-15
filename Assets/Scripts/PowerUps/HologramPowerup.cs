using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HologramPowerup : MonoBehaviour
{
    public float hologramDuration = 5f;
    public GameObject hologramPrefab;

    public Sprite icon;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;

        var powerUpController = other.gameObject.GetComponent<TemporaryPowerUpController>();

        if (powerUpController.AddPowerUp(new HologramPowerupPayload(hologramPrefab,hologramDuration),icon))
        {
            Destroy(gameObject);
        }

    }
}
