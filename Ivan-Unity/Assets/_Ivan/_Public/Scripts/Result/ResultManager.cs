using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ResultManager : MonoBehaviour
{
    public GameObject   resultUI;
    public TMP_Text     scoreText;
    public ScoreManager scoreManager;
    public Image        resultImage;
    public Camera       mainCamera;

    private void Awake()
    {
        if (resultUI != null)
        {
            resultUI.SetActive(false);
        }
    }

    public void ShowResult()
    {
        StartCoroutine(CaptureAndShowResult());
    }

    private IEnumerator CaptureAndShowResult()
    {
        yield return new WaitForEndOfFrame();

        if (scoreManager != null)
        {
            scoreText.text = scoreManager.CurrentScore.ToString();
        }

        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        ScreenCapture.CaptureScreenshotIntoRenderTexture(rt);

        RenderTexture.active = rt;
        Texture2D screenTex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        screenTex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        screenTex.Apply();

        RenderTexture.active = null;
        rt.Release();

        Texture2D flippedTex = new Texture2D(screenTex.width, screenTex.height);
        for (int y = 0; y < screenTex.height; y++)
        {
            flippedTex.SetPixels(0, y, screenTex.width, 1, screenTex.GetPixels(0, screenTex.height - y - 1, screenTex.width, 1));
        }
        flippedTex.Apply();

        Sprite screenSprite = Sprite.Create(flippedTex, new Rect(0, 0, flippedTex.width, flippedTex.height), new Vector2(0.5f, 0.5f));
        resultImage.sprite = screenSprite;
        resultImage.color = Color.white;

        if (resultUI != null)
        {
            resultUI.SetActive(true);
        }
    }
}