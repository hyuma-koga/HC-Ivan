using UnityEngine;
using System.Collections;

public class ReadyManager : MonoBehaviour
{
    [Header("êÿÇËë÷Ç¶ÇÈUI")]
    public GameObject titleUI;
    public GameObject resultUI;
    public GameObject gameUI;

    [Header("éQè∆")]
    public ReadyAnimation readyAnimation;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameUI != null) gameUI.SetActive(false);
    }

    public void StartReadyFromTitle()
    {
        StartCoroutine(StartSequence(true));
    }

    public void StartReadyFromResult()
    {
        StartCoroutine(StartSequence(false));
    }

    private IEnumerator StartSequence(bool fromTitle)
    {
        yield return StartCoroutine(readyAnimation.PlayReadyOnly());

        if (fromTitle)
        {
            if (titleUI != null) titleUI.SetActive(false);
        }
        else
        {
            if (resultUI != null) resultUI.SetActive(false);
        }

        if (gameUI != null)
        {
            gameUI.SetActive(true);
        }

        yield return StartCoroutine(readyAnimation.PlayGoOnly());

        if (gameManager != null)
        {
            gameManager.StartGame();
        }
    }
}