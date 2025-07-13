using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverManager gameOverManager;
    public bool IsPlaying { get; private set; } = false;
    public float clickDisableTime = 0.5f;

    private float clickTimer = 0f;

    public void StartGame()
    {
        IsPlaying = true;
        clickTimer = clickDisableTime;
    }

    private void Update()
    {
        if (IsPlaying && clickTimer > 0f)
        {
            clickTimer -= Time.unscaledDeltaTime;
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