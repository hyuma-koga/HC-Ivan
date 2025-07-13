using UnityEngine;
using System.Collections;
using TMPro;

public class ReadyAnimation : MonoBehaviour
{
    [Header("Ready UI")]
    public GameObject readyUI;
    public RectTransform readyLeftPanel;
    public RectTransform readyRightPanel;
    public RectTransform readyText;

    [Header("Go UI")]
    public GameObject goUI;
    public RectTransform goLeftPanel;
    public RectTransform goRightPanel;
    public GameObject goText;

    [Header("パラメータ")]
    public float panelMoveSpeed = 800f;
    public float textMoveSpeed = 1000f;

    public IEnumerator PlayReadyOnly()
    {
        readyUI.SetActive(true);
        goUI.SetActive(false);

        float panelHalfWidth = readyLeftPanel.rect.width / 2;

        readyLeftPanel.anchoredPosition = new Vector2(-panelHalfWidth * 2, 0);
        readyRightPanel.anchoredPosition = new Vector2(panelHalfWidth * 2, 0);
        readyText.anchoredPosition = new Vector2(0, Screen.height / 2 + 200);

        while (readyLeftPanel.anchoredPosition.x < -panelHalfWidth)
        {
            float moveAmount = panelMoveSpeed * Time.deltaTime;
            readyLeftPanel.anchoredPosition += new Vector2(moveAmount, 0);
            readyRightPanel.anchoredPosition -= new Vector2(moveAmount, 0);

            if (readyLeftPanel.anchoredPosition.x > -panelHalfWidth)
            {
                readyLeftPanel.anchoredPosition = new Vector2(-panelHalfWidth, 0);
                readyRightPanel.anchoredPosition = new Vector2(panelHalfWidth, 0);
            }
            yield return null;
        }

        while (readyText.anchoredPosition.y > 0)
        {
            float moveAmount = textMoveSpeed * Time.deltaTime;
            readyText.anchoredPosition -= new Vector2(0, moveAmount);
            if (readyText.anchoredPosition.y < 0)
                readyText.anchoredPosition = Vector2.zero;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        readyUI.SetActive(false);
    }

    public IEnumerator PlayGoOnly()
    {
        goUI.SetActive(true);

        float goPanelHalfWidth = goLeftPanel.rect.width / 2;

        // 初期位置
        goLeftPanel.anchoredPosition = new Vector2(-goPanelHalfWidth, 0);
        goRightPanel.anchoredPosition = new Vector2(goPanelHalfWidth, 0);
        goText.SetActive(true);

        // 目標位置を固定値にする（例: ±500）
        float targetLeftX = -500f;
        float targetRightX = 500f;

        while (goLeftPanel.anchoredPosition.x > targetLeftX)
        {
            float moveAmount = panelMoveSpeed * Time.deltaTime;
            goLeftPanel.anchoredPosition -= new Vector2(moveAmount, 0);
            goRightPanel.anchoredPosition += new Vector2(moveAmount, 0);

            if (goLeftPanel.anchoredPosition.x <= targetLeftX)
            {
                goLeftPanel.anchoredPosition = new Vector2(targetLeftX, 0);
                goRightPanel.anchoredPosition = new Vector2(targetRightX, 0);
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        goUI.SetActive(false);
    }
}