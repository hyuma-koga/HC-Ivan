using UnityEngine;

public class FruitMergeManager : MonoBehaviour
{
    public static FruitMergeManager Instance;
    public FruitManager             fruitManager;
    public GameObject               fruitPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Marge(FruitController a, FruitController b)
    {
        if (a == null || b == null)
        {
            return;
        }

        //中間位置
        Vector3 margePos = (a.transform.position + b.transform.position) / 2;

        //次の種類取得
        FruitType nextType = fruitManager.GetFruitData(a.fruitType).nextType;

        //スイカならチェリーに戻る
        if (a.fruitType == FruitType.Watermelon)
        {
            nextType = FruitType.Cherry;
        }

        //新しいフルーツ生成
        GameObject newFruitObj = Instantiate(fruitPrefab, margePos, Quaternion.identity);
        FruitController newFruit = newFruitObj.GetComponent<FruitController>();
        newFruit.Init(fruitManager.GetFruitData(nextType));

        Destroy(a.gameObject);
        Destroy(b.gameObject);
    }
}
