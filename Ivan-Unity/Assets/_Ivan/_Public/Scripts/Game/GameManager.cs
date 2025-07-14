using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameOverManager gameOverManager;
    public FruitDropper    fruitDropper;
    public ScoreManager    scoreManager;
    public bool            IsPlaying { get; private set; } = false;
    public bool            IsClickDisabled { get; private set; } = false;
    public float           clickDisableTime = 0.5f;

    private float          clickTimer = 0f;
    private bool           firstFrameIgnore = false;

    public void StartGame(bool isContinue = false)
    {
        if (fruitDropper != null)
        {
            //不要フルーツ削除（スタンバイ除外、高さ基準）
            fruitDropper.RemoveHighFruits();
        }

        if (scoreManager != null && !isContinue)
        {
            scoreManager.ResetScore();
        }

        if (fruitDropper != null && !isContinue)
        {
            //新規スタート時のみ初期化
            fruitDropper.InitializeDropper();
        }

        Time.timeScale = 1f;
        IsPlaying = true;
        clickTimer = clickDisableTime;
        firstFrameIgnore = true;

        if (gameOverManager != null)
        {
            gameOverManager.ResetGameOverFlag();
        }
    }

    private void Update()
    {
        if (IsPlaying && clickTimer > 0f)
        {
            clickTimer -= Time.unscaledDeltaTime;
        }

        if (IsPlaying && firstFrameIgnore)
        {
            firstFrameIgnore = false;
            return;
        }

        if (gameOverManager != null && IsPlaying && !gameOverManager.IsGameOver)
        {
            gameOverManager.CheckGameOver();
        }
    }

    public bool CanClick()
    {
        return clickTimer <= 0f;
    }

    public void EndGame()
    {
        IsPlaying = false;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.score = scoreManager.CurrentScore;
        data.activeFruits = fruitDropper.GetActiveFruitsData();
        data.standbyFruit = fruitDropper.GetStandbyFruitData();
        data.nextFruitType = fruitDropper.GetNextFruitType();

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("SaveData")) return;

        string json = PlayerPrefs.GetString("SaveData");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        scoreManager.SetScore(data.score);
        fruitDropper.RestoreFruits(data.activeFruits, data.standbyFruit, data.nextFruitType);
    }

    public void LoadGameAndStart()
    {
        LoadGame();

        if (fruitDropper != null)
        {
            FruitType lastType = fruitDropper.RemoveLastDroppedFruitAndGetType();
            fruitDropper.ClearOnlyStandbyFruit();
            fruitDropper.CreateStandbyFruit(lastType);
        }

        Time.timeScale = 1f;
        IsPlaying = true;
        clickTimer = clickDisableTime;
        firstFrameIgnore = true;

        if (gameOverManager != null)
        {
            gameOverManager.ResetGameOverFlag();
        }
    }

    public void DisableClickTemporarily(float duration)
    {
        StartCoroutine(DisableClickCoroutine(duration));
    }

    private IEnumerator DisableClickCoroutine(float duration)
    {
        IsClickDisabled = true;
        yield return new WaitForSeconds(duration);
        IsClickDisabled = false;
    }
}