using TMPro;
using UnityEngine;

public class ScoreController : Singleton<ScoreController>
{
    public TextMeshProUGUI txt;

    public int CurrentScore { get; private set; } = 0;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        txt.text = CurrentScore.ToString();
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        UpdateUI();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        UpdateUI();
    }
}
