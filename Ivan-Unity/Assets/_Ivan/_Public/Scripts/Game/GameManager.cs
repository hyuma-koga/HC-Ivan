using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverManager gameOverManager;
    public FruitDropper fruitDropper;
    public ScoreManager scoreManager;

    public bool IsPlaying { get; private set; } = false;
    public float clickDisableTime = 0.5f;

    private float clickTimer = 0f;
    private bool firstFrameIgnore = false;

    public void StartGame()
    {
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        if (fruitDropper != null)
        {
            fruitDropper.InitializeDropper();
        }

        Time.timeScale = 1f;

        IsPlaying = true;
        clickTimer = clickDisableTime;
        firstFrameIgnore = true;

        if (gameOverManager != null)
        {
            gameOverManager.ResetGameOverFlag();
        }
    }

    private void Update()
    {
        if (IsPlaying && clickTimer > 0f)
        {
            clickTimer -= Time.unscaledDeltaTime;
        }

        if (IsPlaying && firstFrameIgnore)
        {
            firstFrameIgnore = false;
            return;
        }

        if (gameOverManager != null && IsPlaying && !gameOverManager.IsGameOver)
        {
            gameOverManager.CheckGameOver();
        }
    }

    public bool CanClick()
    {
        return clickTimer <= 0f;
    }

    public void EndGame()
    {
        IsPlaying = false;
    }
}