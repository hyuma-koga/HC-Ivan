using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image        nextFruitImage;
    public Text         scoreText;
    public Text         bestScoreText;
    public ScoreManager scoreManager;

    private void Update()
    {
        if (scoreText != null && scoreManager != null)
        {
            scoreText.text = scoreManager.CurrentScore.ToString();
        }

        if (bestScoreText != null && scoreManager != null)
        {
            bestScoreText.text = scoreManager.BestScore.ToString();
        }
    }

    public void UpdateNextFruit(Sprite nextSprite)
    {
        if (nextFruitImage != null)
        {
            nextFruitImage.sprite = nextSprite;
        }
    }
}
