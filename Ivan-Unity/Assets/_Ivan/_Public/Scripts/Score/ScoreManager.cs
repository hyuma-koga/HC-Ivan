using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int  CurrentScore => score;

    private int score = 0;

    public void AddScore(int value)
    {
        score += value;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
