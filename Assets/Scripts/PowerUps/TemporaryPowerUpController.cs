using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class TemporaryPowerUpController : MonoBehaviour
{
    private const int maxPowerUps = 2;
    private List<ITemporaryPowerUp> powerUps;
    private PlayerController player;
    void Start()
    {
        powerUps = new List<ITemporaryPowerUp>();
        player = gameObject.GetComponent<PlayerController>();
    }

    


    public bool AddPowerUp(ITemporaryPowerUp powerUp)
    {
        //check if powerup can be added
        if (this.powerUps.Count >= maxPowerUps || !CompatibleWithPowerUps(powerUp)) return false;
        
        // add powerup to list
        StartCoroutine(ActivateNextPowerUp(powerUp));
        return true;
    }

    private bool CompatibleWithPowerUps(ITemporaryPowerUp powerUp)
    {
        return powerUps.TrueForAll(p => p.Compatible(powerUp));
    }

    private IEnumerator ActivateNextPowerUp(ITemporaryPowerUp powerUp)
    {
        powerUps.Add(powerUp);
        powerUp.OnAttach(this.player);
        yield return new WaitForSeconds(powerUp.Duration());
        powerUp.OnDetach(player);
        powerUps.Remove(powerUp);
    }
}
