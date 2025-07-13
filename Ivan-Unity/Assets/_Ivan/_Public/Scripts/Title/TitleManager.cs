using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public GameObject titleUI;
    public GameObject gameUI;
    public GameObject resultUI;
    public ReadyManager readyManager;

    private void Start()
    {
        ShowTitleUI();
    }

    public void StartGame()
    {
        if (gameUI != null)
        {
            gameUI.SetActive(false); // Readyââèoå„Ç…ONÇ…Ç∑ÇÈ
        }

        if (readyManager != null)
        {
            readyManager.StartReadyFromTitle();
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