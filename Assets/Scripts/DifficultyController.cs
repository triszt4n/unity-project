using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public ScoreController scoreController;
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        VeryHard,
        Impossible
    }

    public Difficulty GetDifficulty()
    {
        if (scoreController.CurrentScore < 1500)
        {
            return Difficulty.Easy;
        }

        if (scoreController.CurrentScore < 3500)
        {
            return Difficulty.Normal;
        }

        if (scoreController.CurrentScore < 6000)
        {
            return Difficulty.Hard;
        }

        if (scoreController.CurrentScore < 10000)
        {
            return Difficulty.VeryHard;
        }

        return Difficulty.Impossible;
    }
}