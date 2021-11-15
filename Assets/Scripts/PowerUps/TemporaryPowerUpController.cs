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
        public ITemporaryPowerUp payload;
        public Sprite icon;

        public PowerUp(ITemporaryPowerUp payload, Sprite icon)
        {
            this.payload = payload;
            this.icon = icon;
        }
    }
    
    public Image[] powerUpIconHolders = new Image[maxPowerUps];


    private List<PowerUp> powerUps;
    private PlayerController player;
    void Start()
    {
        powerUps = new List<PowerUp>();
        player = gameObject.GetComponent<PlayerController>();
        UpdateUI();
    }


    private void UpdateUI()
    {
        for (int i = 0; i < maxPowerUps; i++)
        {
            if (i < powerUps.Count)
            {
                powerUpIconHolders[i].sprite = powerUps[i].icon;
                powerUpIconHolders[i].color = Color.white;
            }
            else
            {
                powerUpIconHolders[i].sprite = null;
                powerUpIconHolders[i].color = Color.clear;
            }
        }
    }


    public bool AddPowerUp(ITemporaryPowerUp payload, Sprite powerUpIcon)
    {
        var powerUp = new PowerUp(payload, powerUpIcon);
        
        //check if powerup can be added
        if (this.powerUps.Count >= maxPowerUps || !CompatibleWithPowerUps(powerUp)) return false;
        
        // add powerup to list
        StartCoroutine(ActivateNextPowerUp(powerUp));
        return true;
    }

    private bool CompatibleWithPowerUps(PowerUp powerUp)
    {
        return powerUps.TrueForAll(p => p.payload.Compatible(powerUp.payload));
    }

    private IEnumerator ActivateNextPowerUp(PowerUp powerUp)
    {
        powerUps.Add(powerUp);
        UpdateUI();
        powerUp.payload.OnAttach(player);
        yield return new WaitForSeconds(powerUp.payload.Duration());
        powerUp.payload.OnDetach(player);
        powerUps.Remove(powerUp);
        UpdateUI();
    }

}
