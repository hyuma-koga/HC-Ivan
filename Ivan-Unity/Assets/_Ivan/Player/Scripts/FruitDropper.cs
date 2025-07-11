using UnityEngine;

public class FruitDropper : MonoBehaviour
{
    public GameObject   fruitPrefab;
    public Transform    spawnPoint;
    public FruitManager fruitManager;

    private FruitType   nextFruitType = FruitType.Cherry;

    public void DropFruit()
    {
        FruitData data = fruitManager.GetFruitData(nextFruitType);

        GameObject fruitObj = Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity);

        FruitController fruitCtrl = fruitObj.GetComponent<FruitController>();
        fruitCtrl.Init(data);

        nextFruitType = GetRandomFruitType();
    }

    private FruitType GetRandomFruitType()
    {
        int random = Random.Range(0, 5);
        return (FruitType)random;
    }
}
