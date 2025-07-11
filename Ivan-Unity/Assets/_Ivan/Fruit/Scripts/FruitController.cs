using UnityEngine;

public class FruitController : MonoBehaviour
{
    public FruitType fruitType;
    public SpriteRenderer spriteRenderer;
    public int Score;

    //‰Šú‰»ƒƒ\ƒbƒh
    public void Init(FruitData data)
    {
        fruitType = data.type;
        Score = data.score;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        spriteRenderer.sprite = data.sprite;
    }
}