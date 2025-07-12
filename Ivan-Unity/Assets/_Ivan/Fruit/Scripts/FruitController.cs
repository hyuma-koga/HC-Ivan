using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public FruitType      fruitType;
    public SpriteRenderer spriteRenderer;
    public int            score;
    public bool           isMerging = false;

    //���������\�b�h
    public void Init(FruitData data)
    {
        fruitType = data.type;
        score = data.score;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        spriteRenderer.sprite = data.sprite;
        UpdatePolygonColliderShape();
    }

    private void UpdatePolygonColliderShape()
    {
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        
        if (polygonCollider == null)
        {
            return;
        }

        List<Vector2> shape = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, shape);

        // �����̃p�X���N���A�ipathCount = 1 �ɂ���j
        polygonCollider.pathCount = 1;

        // �V�����p�X���Z�b�g
        polygonCollider.SetPath(0, shape.ToArray());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FruitController other = collision.gameObject.GetComponent<FruitController>();
        
        if (other != null && other.fruitType == this.fruitType)
        {
            if (this.isMerging || other.isMerging)
            {
                return;
            }

            this.isMerging = true;
            other.isMerging = true;

            FruitMergeManager.Instance.Marge(this, other);
        }
    }
}