using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float limitX = 7f;

    public void Move()
    {
        float moveX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        Vector3 pos = transform.position;
        pos.x += moveX;
        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        transform.position = pos;
    }
}