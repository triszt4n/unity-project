using UnityEngine;

public class DeathMenuController : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
    }

    public void ContinuePressed()
    {
        gameObject.SetActive(false);
        player.ContinueGame();
    }

    public void QuitPressed()
    {
        player.QuitGame();
    }
}
