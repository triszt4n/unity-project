using System;
using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;



public class HealthPowerup : AbstractPowerup
{
    protected override bool TryApplyEffect(GameObject playerObject)
    {
        var player = playerObject.GetComponent<PlayerController>();
        //apply effect
        return player.health < player.maxHealth && player.TryHeal();
        
    }
}

