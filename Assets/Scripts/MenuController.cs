using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI continueButtonText;
    public GameObject continueButtonObject;
    public static string saveGameFileName = "HIGHSCORE.lol";

    [Serializable]
    public class HighScore
    {
        public DateTime time;
        public int score;
    }
    private int maxHighScore = 0;

    private void Start()
    {
        string fileName = Application.persistentDataPath + saveGameFileName;
        string newValue = "";
        if (File.Exists(fileName))
        {
            var fileContent = File.Open(fileName, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HighScore>));
            List<HighScore> highScores = xmlSerializer.Deserialize(fileContent) as List<HighScore>;
            if (highScores != null)
            {
                foreach (var highScore in highScores)
                {
                    if (highScore.score > maxHighScore) maxHighScore = highScore.score;
                }
            }
            newValue = (maxHighScore / 2).ToString();
            continueButtonObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            continueButtonObject.SetActive(false);
            continueButtonObject.GetComponent<Button>().interactable = false;
        }
        continueButtonText.text = continueButtonText.text.Replace("$score", newValue);
    }

    public void ContinueGame()
    {
        ScoreController.startingScore = maxHighScore / 2;
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void Play()
    {
        ScoreController.startingScore = 0;
        SceneManager.LoadScene("Scenes/GameScene");
    }
    
}
