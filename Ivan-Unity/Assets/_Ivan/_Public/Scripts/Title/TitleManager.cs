using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject   titleUI;
    public GameObject   gameUI;
    public GameObject   resultUI;
    public ScoreManager scoreManager;
    public GameManager  gameManager;
    public FruitDropper fruitDropper;

    private void Start()
    {
        ShowTitleUI();
    }

    public void StartGame()
    {
        if (titleUI != null)
        {
            titleUI.SetActive(false);
        }

        if (gameUI != null)
        {
            gameUI.SetActive(true);
        }

        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }

        if (gameManager != null)
        {
            gameManager.StartGame();
        }

        if (fruitDropper != null)
        {
            fruitDropper.InitializeDropper();
        }

        Time.timeScale = 1f;
    }

    private void ShowTitleUI()
    {
        if (titleUI != null)
        {
            titleUI.SetActive(true);
        }

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }

        if (resultUI != null)
        {
            resultUI.SetActive(false);
        }
    }
}
