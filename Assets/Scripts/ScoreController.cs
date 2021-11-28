using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI txt;

    public static int startingScore = 0;
    public int CurrentScore { get; private set; } = 0;

    private void Start()
    {
        UpdateUI();
        CurrentScore = startingScore;
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

    public void HalfScore()
    {
        CurrentScore = CurrentScore / 2;
        UpdateUI();
    }
}
