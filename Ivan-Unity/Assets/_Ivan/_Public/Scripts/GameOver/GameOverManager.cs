using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public float         gameOverLineY = 2.2f;
    public GameObject    gameOverUI;
    public bool          IsGameOver => isGameOver;
    public ResultManager resultManager;

    private bool      isGameOver = false;
    
    public void CheckGameOver()
    {
        FruitController[] allFruits = Object.FindObjectsByType<FruitController>(FindObjectsSortMode.None);

        foreach (FruitController fruit in allFruits)
        {
            Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();

            if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic && fruit.canCheckGameOver)
            {
                if (fruit.GetTopY() > gameOverLineY)
                {
                    TriggerGameOver();
                    break;
                }
            }
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        Debug.Log("Game Over!");

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        StartCoroutine(ShowResultAfterDelay(3f));

        Time.timeScale = 0f;
    }

    public IEnumerator ShowResultAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if (resultManager != null)
        {
            resultManager.ShowResult();
        }
    }
}