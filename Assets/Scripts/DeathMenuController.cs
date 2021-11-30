using Enemy;
using PowerUps;
using UnityEngine;

public class DeathMenuController : MonoBehaviour
{
    public PlayerController player;
    public EnemySpawner enemySpawner;
    public PowerUpSpawner powerUpSpawner;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
        enemySpawner.StopGeneration();
        powerUpSpawner.StopGeneration();
    }

    public void ContinuePressed()
    {
        gameObject.SetActive(false);
        enemySpawner.StartGeneration();
        powerUpSpawner.StartGeneration();
        player.ContinueGame();
    }

    public void QuitPressed()
    {
        player.QuitGame(false);
    }
}
