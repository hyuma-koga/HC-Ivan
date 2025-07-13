using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject  titleUI;
    public GameObject  gameUI;
    public GameObject  resultUI;
    public GameManager gameManager;

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

        if (gameManager != null)
        {
            gameManager.StartGame();
        }
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