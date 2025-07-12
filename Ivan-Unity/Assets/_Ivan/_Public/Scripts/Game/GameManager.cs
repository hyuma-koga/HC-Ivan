using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverManager gameOverManager;

    private void Update()
    {
        if (gameOverManager != null && !gameOverManager.IsGameOver)
        {
            gameOverManager.CheckGameOver();
        }
    }
}