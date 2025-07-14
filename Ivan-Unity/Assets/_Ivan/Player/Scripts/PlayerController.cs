using UnityEngine;
using UnityEngine.EventSystems;

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
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return; // UIクリックならフルーツ落下させない
            }
            dropper.DropFruit();
        }
    }
}
