using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private Button startButton;
    private Button scoreboardButton;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-button");
        scoreboardButton = root.Q<Button>("scoreboard-button");

        startButton.clicked += StartButtonPressed;
        scoreboardButton.clicked += ScoreboardButtonPressed;
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }

    void ScoreboardButtonPressed()
    {
        
    }
}
