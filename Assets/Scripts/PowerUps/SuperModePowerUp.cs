using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;


public class SuperModePowerUp : AbstractPowerup
{
    public float superModeDuration = 5f;
    public float moveMultiplier = 1.5f;

    public Sprite icon;


    protected override bool TryApplyEffect(GameObject playerObject)
    {
        var powerUpController = playerObject.GetComponent<TemporaryPowerUpController>();
        
        return  powerUpController.AddPowerUp(new SuperModePowerUpPayload(superModeDuration, moveMultiplier), icon);
    }
}
