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
        var fileName = Application.persistentDataPath + MenuController.SAVE_GAME_FILENAME;
        if (!File.Exists(fileName)) return;

        var fileContent = File.Open(fileName, FileMode.Open);        
        var xmlSerializer = new XmlSerializer(typeof(List<MenuController.HighScore>));
        var highScores = xmlSerializer.Deserialize(fileContent) as List<MenuController.HighScore>;
        fileContent.Close();
        if (highScores == null) return;

        foreach (var highScore in highScores)
        {
            textContent.text += $"{highScore.time}\t{highScore.score}\n";
        }
    }

    public void CloseScoreboard()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }
}