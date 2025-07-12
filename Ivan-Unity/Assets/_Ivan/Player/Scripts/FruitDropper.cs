using UnityEngine;
using System.Collections;

public class FruitDropper : MonoBehaviour
{
    public GameObject   fruitPrefab;
    public Transform    spawnPoint;
    public FruitManager fruitManager;
    public UIManager    uiManager;

    private FruitType   currentFruitType;
    private FruitType   nextFruitType;
    private GameObject  standbyFruit;

    private void Start()
    {
        currentFruitType = GetRandomFruitType();
        nextFruitType = GetRandomFruitType();

        UpdateNextFruitUI();
        CreateStandbyFruit(currentFruitType);
    }

    private void Update()
    {
        if (standbyFruit != null)
        {
            standbyFruit.transform.position = spawnPoint.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DropFruit();
        }
    }

    //�t���[�c�̗���
    public void DropFruit()
    {
        if (standbyFruit == null)
        {
            return;
        }

        Rigidbody2D rb = standbyFruit.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        standbyFruit = null;

        StartCoroutine(SpawnNextFruitAfterDelay(0.5f));
    }

    //�����^�C�~���O��x�点��
    private IEnumerator SpawnNextFruitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        currentFruitType = nextFruitType;
        nextFruitType = GetRandomFruitType();

        UpdateNextFruitUI();
        CreateStandbyFruit(currentFruitType);
    }

    //�v���C���[�Ɏ������ăX�^���o�C��Ԃɂ���
    private void CreateStandbyFruit(FruitType type)
    {
        FruitData data = fruitManager.GetFruitData(type);

        standbyFruit = Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity);

        FruitController fruitCtrl = standbyFruit.GetComponent<FruitController>();
        fruitCtrl.Init(data);

        Rigidbody2D rb = standbyFruit.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    //UI�Ɏ��̃t���[�c��\��
    private void UpdateNextFruitUI()
    {
        FruitData nextData = fruitManager.GetFruitData(nextFruitType);
        uiManager.UpdateNextFruit(nextData.sprite);
    }

    //���̃t���[�c�������_���Ɏ擾����
    private FruitType GetRandomFruitType()
    {
        int random = Random.Range(0, 5);
        return (FruitType)random;
    }
}
