using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardController : MonoBehaviour
{
    public TextMeshProUGUI textContent;

    // Start is called before the first frame update
    void Start()
    {
        var repository = HighScoreRepository.Instance;
        if (!repository.HasHighScore) return;

        foreach (var highScore in repository.HighScoreList)
        {
            textContent.text +=  $"{highScore.time}\t{highScore.score}\n";
        }
        
    }

    public void CloseScoreboard()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }
}