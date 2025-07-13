using UnityEngine;
using System.Collections;

public class ReadyManager : MonoBehaviour
{
    public GameObject     titleUI;
    public GameObject     resultUI;
    public GameObject     gameUI;
    public ReadyAnimation readyAnimation;

    private GameManager   gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }
    }

    public void StartReadyFromTitle()
    {
        StartCoroutine(StartSequence(true));
    }

    public void StartReadyFromResult()
    {
        StartCoroutine(StartSequence(false));
    }

    public void StartReadyFromContinue()
    {
        StartCoroutine(StartSequenceFromContinue());
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

    private IEnumerator StartSequenceFromContinue()
    {
        yield return StartCoroutine(readyAnimation.PlayReadyOnly());

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
            gameManager.LoadGameAndStart();
        }

        yield return StartCoroutine(readyAnimation.PlayGoOnly());
    }
}