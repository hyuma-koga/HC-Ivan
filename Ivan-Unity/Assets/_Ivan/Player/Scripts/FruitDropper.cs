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

    //フルーツの落下
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

    //生成タイミングを遅らせる
    private IEnumerator SpawnNextFruitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        currentFruitType = nextFruitType;
        nextFruitType = GetRandomFruitType();

        UpdateNextFruitUI();
        CreateStandbyFruit(currentFruitType);
    }

    //プレイヤーに持たせてスタンバイ状態にする
    private void CreateStandbyFruit(FruitType type)
    {
        FruitData data = fruitManager.GetFruitData(type);

        standbyFruit = Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity);

        FruitController fruitCtrl = standbyFruit.GetComponent<FruitController>();
        fruitCtrl.Init(data);

        Rigidbody2D rb = standbyFruit.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    //UIに次のフルーツを表示
    private void UpdateNextFruitUI()
    {
        FruitData nextData = fruitManager.GetFruitData(nextFruitType);
        uiManager.UpdateNextFruit(nextData.sprite);
    }

    //次のフルーツをランダムに取得する
    private FruitType GetRandomFruitType()
    {
        int random = Random.Range(0, 5);
        return (FruitType)random;
    }
}
