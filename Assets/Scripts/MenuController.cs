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
    
    private int maxHighScore = 0;

    private void Start()
    {
        var repository = HighScoreRepository.Instance;
        if (repository.HasHighScore)
        {
            maxHighScore = repository.HighScore.score;
            continueButtonObject.GetComponent<Button>().interactable = true;
            continueButtonText.text = continueButtonText.text.Replace("$score",(maxHighScore / 2).ToString());
        }
        else
        {
            continueButtonObject.SetActive(false);
            continueButtonObject.GetComponent<Button>().interactable = false;
        }
    }

    public void ContinueGame()
    {
        ScoreController.startingScore = maxHighScore / 2;
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void Play()
    {
        GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<MenuMusicController>().StopMusic();
        ScoreController.startingScore = 0;
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void OpenScoreboard()
    {
        SceneManager.LoadScene("Scenes/ScoreboardScene");
    }
}
