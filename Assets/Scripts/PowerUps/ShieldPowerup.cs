using System;
using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;

public class ShieldPowerup : AbstractPowerup
{
    public float shieldDuration = 5f;
    public GameObject shieldPrefab;
    public Sprite icon;


    protected override bool TryApplyEffect(GameObject playerObject)
    {
        var powerUpController = playerObject.GetComponent<TemporaryPowerUpController>();

        return powerUpController.AddPowerUp(new ShieldPowerUpPayload(shieldDuration,shieldPrefab), icon);
    }
    
}