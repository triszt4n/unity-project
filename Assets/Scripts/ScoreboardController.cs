using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseScoreboard()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }
}
