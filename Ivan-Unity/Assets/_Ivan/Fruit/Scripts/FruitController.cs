using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public FruitType      fruitType;
    public SpriteRenderer spriteRenderer;
    public int            score;
    public bool           isMerging = false;
    public bool           canCheckGameOver = false;

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

    //PolygonCollider�̐ݒ�
    private void UpdatePolygonColliderShape()
    {
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        
        if (polygonCollider == null)
        {
            return;
        }

        List<Vector2> shape = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, shape);
        polygonCollider.pathCount = 1;
        polygonCollider.SetPath(0, shape.ToArray());
    }

    //����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FruitController other = collision.gameObject.GetComponent<FruitController>();
        
        if (other != null && other.fruitType == this.fruitType)
        {
            Rigidbody2D thisRb = GetComponent<Rigidbody2D>();
            Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();

            //�������ĂȂ��t���[�c�̍��̋֎~
            if (thisRb.bodyType != RigidbodyType2D.Dynamic || otherRb.bodyType != RigidbodyType2D.Dynamic)
            {
                return;
            }

            if (this.isMerging || other.isMerging)
            {
                return;
            }

            this.isMerging = true;
            other.isMerging = true;

            FruitMergeManager.Instance.Marge(this, other);
        }
    }

    //�Q�[���I�[�o�[�p�Ɏg�p���鍂��
    public float GetTopY()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        return transform.position.y + spriteRenderer.bounds.extents.y;
    }

    public IEnumerator EnableGameOverCheckAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canCheckGameOver = true;
    }
}