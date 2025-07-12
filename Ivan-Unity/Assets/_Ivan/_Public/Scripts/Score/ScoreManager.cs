using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int  CurrentScore => score;
    public int  BestScore => bestScore;

    private int score = 0;
    private int bestScore = 0;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public void AddScore(int value)
    {
        score += value;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }

    public void ResetScore()
    {
        score = 0;
    }
}
