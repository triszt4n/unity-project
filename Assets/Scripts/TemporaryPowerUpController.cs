using System;
using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class TemporaryPowerUpController : MonoBehaviour
{
    
    private const int maxPowerUps = 2;

    private struct PowerUp
    {
        public TemporaryPowerUp payload;
        public Sprite icon;

        public PowerUp(TemporaryPowerUp payload, Sprite icon)
        {
            this.payload = payload;
            this.icon = icon;
        }
    }
    
    public Image[] powerUpIconHolders = new Image[maxPowerUps];
    private Queue<PowerUp> powerUps;
    private PlayerController player;
    private bool powerUpActive = false;
    
    void Start()
    {
        powerUps = new Queue<PowerUp>();
        player = gameObject.GetComponent<PlayerController>();
        UpdateUI();
    }


    private void UpdateUI()
    {
        var powerUpArray = powerUps.ToArray();
        for (int i = 0; i < maxPowerUps; i++)
        {
            if (i < powerUpArray.Length)
            {
                powerUpIconHolders[i].sprite = powerUpArray[i].icon;
                powerUpIconHolders[i].color = Color.white;
            }
            else
            {
                powerUpIconHolders[i].sprite = null;
                powerUpIconHolders[i].color = Color.clear;
            }
        }
    }
    
    
    
    public bool AddPowerUp(TemporaryPowerUp payload, Sprite powerUpIcon)
    {
        var powerUp = new PowerUp(payload, powerUpIcon);
        
        //check if powerup can be added
        if (this.powerUps.Count >= maxPowerUps) return false;
        powerUps.Enqueue(powerUp);
        UpdateUI();
        UpdateActivePowerUp();
        
        return true;
    }


    private void UpdateActivePowerUp()
    {
        if (!powerUpActive && powerUps.Count > 0)
        {
            powerUpActive = true;
            StartCoroutine(ActivateNextPowerUp());
        }
    }

    public void ResetFifo()
    {
        StopAllCoroutines();
        foreach (var powerUp in powerUps)
        {
            powerUp.payload.OnDetach(player);
        }
        powerUps.Clear();
        UpdateUI();
    }
    
    private IEnumerator ActivateNextPowerUp()
    {
        var powerUp = powerUps.Peek();
        UpdateUI();
        powerUp.payload.OnAttach(player);
        yield return new WaitForSeconds(powerUp.payload.Duration);
        powerUp.payload.OnDetach(player);
        powerUps.Dequeue();
        UpdateUI();
        powerUpActive = false;
        UpdateActivePowerUp();
    }

}
