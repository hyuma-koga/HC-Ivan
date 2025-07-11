using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMover  mover;
    private FruitDropper dropper;

    private void Awake()
    {
        mover = GetComponent<PlayerMover>();
        dropper = GetComponent<FruitDropper>();
    }

    private void Update()
    {
        mover.Move();

        if (Input.GetMouseButtonDown(0))
        {
            dropper.DropFruit();
        }
    }
}
