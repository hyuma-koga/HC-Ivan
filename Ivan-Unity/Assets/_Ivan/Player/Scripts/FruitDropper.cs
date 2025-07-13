using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class FruitDropper : MonoBehaviour
{
    public GameObject   fruitPrefab;
    public Transform    spawnPoint;
    public FruitManager fruitManager;
    public GameManager  gameManager;
    public UIManager    uiManager;

    private FruitType   currentFruitType;
    private FruitType   nextFruitType;
    private GameObject  standbyFruit;

    private void Update()
    {
        if (gameManager == null || !gameManager.IsPlaying || gameManager.IsClickDisabled)
        {
            return;
        }

        if (standbyFruit != null)
        {
            standbyFruit.transform.position = spawnPoint.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            DropFruit();
        }
    }

    public void InitializeDropper()
    {
        currentFruitType = GetRandomFruitType();
        nextFruitType = GetRandomFruitType();

        UpdateNextFruitUI();
        CreateStandbyFruit(currentFruitType);
    }

    public void ClearFruit()
    {
        StopAllCoroutines();

        // スタンバイ中のフルーツ削除
        if (standbyFruit != null)
        {
            Destroy(standbyFruit);
            standbyFruit = null;
        }

        // 画面上の全フルーツ削除
        FruitController[] fruits = FindObjectsByType<FruitController>(FindObjectsSortMode.None);
        foreach (var fruit in fruits)
        {
            Destroy(fruit.gameObject);
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

        FruitController fruitCtrl = standbyFruit.GetComponent<FruitController>();
        fruitCtrl.StartCoroutine(fruitCtrl.EnableGameOverCheckAfterDelay(0.5f));

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

    public List<FruitSaveData> GetActiveFruitsData()
    {
        List<FruitSaveData> dataList = new List<FruitSaveData>();

        FruitController[] fruits = FindObjectsByType<FruitController>(FindObjectsSortMode.None);
        foreach (var fruit in fruits)
        {
            FruitSaveData data = new FruitSaveData();
            data.type = fruit.fruitType;
            data.position = fruit.transform.position;
            data.rotation = fruit.transform.rotation;
            data.scale = fruit.transform.localScale;
            dataList.Add(data);
        }

        return dataList;
    }

    public FruitSaveData GetStandbyFruitData()
    {
        if (standbyFruit == null) return null;

        FruitController fruitCtrl = standbyFruit.GetComponent<FruitController>();
        FruitSaveData data = new FruitSaveData();
        data.type = fruitCtrl.fruitType;
        data.position = standbyFruit.transform.position;
        data.rotation = standbyFruit.transform.rotation;
        data.scale = standbyFruit.transform.localScale;
        return data;
    }

    public FruitType GetNextFruitType()
    {
        return nextFruitType;
    }

    public void RestoreFruits(List<FruitSaveData> activeData, FruitSaveData standbyData, FruitType nextType)
    {
        ClearFruit();

        foreach (var data in activeData)
        {
            var fruitObj = Instantiate(fruitPrefab, data.position, data.rotation);
            fruitObj.transform.localScale = data.scale;

            FruitController fruitCtrl = fruitObj.GetComponent<FruitController>();
            FruitData fullData = fruitManager.GetFruitData(data.type);
            fruitCtrl.Init(fullData);

            fruitObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        if (standbyData != null)
        {
            standbyFruit = Instantiate(fruitPrefab, standbyData.position, standbyData.rotation);
            standbyFruit.transform.localScale = standbyData.scale;

            FruitController fruitCtrl = standbyFruit.GetComponent<FruitController>();
            FruitData fullData = fruitManager.GetFruitData(standbyData.type);
            fruitCtrl.Init(fullData);

            standbyFruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        nextFruitType = nextType;
        UpdateNextFruitUI();
    }
}