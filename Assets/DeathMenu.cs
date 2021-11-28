using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEndMenu(PlayerController player)
    {
        this.player = player;
        gameObject.SetActive(true);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
        player.ContinueGame();
    }
}
