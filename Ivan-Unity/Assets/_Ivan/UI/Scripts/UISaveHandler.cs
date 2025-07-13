using UnityEngine;

public class UISaveHandler : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject titleUI;
    public GameObject gameUI;
    public ReadyManager readyManager;

    public void OnStartButtonPressed()
    {
        if (gameManager != null)
        {
            if (gameManager.scoreManager != null)
            {
                gameManager.scoreManager.ResetScore();
            }

            if (gameManager.fruitDropper != null)
            {
                gameManager.fruitDropper.ClearFruit();
            }
        }

        if (gameUI != null) gameUI.SetActive(false);
        if (readyManager != null) readyManager.StartReadyFromTitle();
    }

    public void OnContinueButtonPressed()
    {
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            Debug.LogWarning("セーブデータがありません");
            return;
        }

        gameManager.LoadGameAndStart();

        if (gameUI != null) gameUI.SetActive(false); // Ready 後に ON
        if (readyManager != null) readyManager.StartReadyFromContinue();
    }

    public void OnSaveButtonPressed()
    {
        if (gameManager != null)
        {
            gameManager.EndGame();
            gameManager.SaveGame();
        }

        if (gameUI != null) gameUI.SetActive(false);
        if (titleUI != null) titleUI.SetActive(true);

        Debug.Log("ゲームをセーブしてタイトルに戻りました");
    }
}
