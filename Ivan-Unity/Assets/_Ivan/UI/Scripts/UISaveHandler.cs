using UnityEngine;

public class UISaveHandler : MonoBehaviour
{
    public GameManager  gameManager;
    public GameObject   titleUI;
    public GameObject   gameUI;
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

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }

        if (readyManager != null)
        {
            readyManager.StartReadyFromTitle();
        }
    }

    public void OnContinueButtonPressed()
    {
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            return;
        }

        // LoadGame 内で Restore だけ行う
        gameManager.LoadGame();

        if (gameManager.fruitDropper != null)
        {
            FruitType lastType = gameManager.fruitDropper.RemoveLastDroppedFruitAndGetType();
            gameManager.fruitDropper.ClearOnlyStandbyFruit();
            gameManager.fruitDropper.CreateStandbyFruit(lastType);
        }

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }

        if (readyManager != null)
        {
            readyManager.StartReadyFromContinue();
        }

        gameManager.StartGame(isContinue: true);
    }

    public void OnSaveButtonPressed()
    {
        if (gameManager != null)
        {
            if (gameManager.fruitDropper != null)
            {
                // 最後に落下させたフルーツだけ削除
                gameManager.fruitDropper.RemoveLastDroppedFruit();
            }

            gameManager.EndGame();
            gameManager.SaveGame();

            if (gameManager.fruitDropper != null)
            {
                gameManager.fruitDropper.ClearOnlyStandbyFruit();
                gameManager.fruitDropper.InitializeDropper();
            }
        }

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }

        if (titleUI != null)
        {
            titleUI.SetActive(true);
        }
    }
}