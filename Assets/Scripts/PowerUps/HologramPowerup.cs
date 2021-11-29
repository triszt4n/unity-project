using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;


public class HologramPowerup : AbstractPowerup
{
    public float hologramDuration = 5f;
    public GameObject hologramPrefab;

    public Sprite icon;

    protected override bool TryApplyEffect(GameObject playerObject)
    {
        var powerUpController = playerObject.GetComponent<TemporaryPowerUpController>();
        return powerUpController.AddPowerUp(new HologramPowerupPayload(hologramPrefab, hologramDuration), icon);
    }
}