using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Fruit", menuName = "Fruit/FruitManager")]
public class FruitManager : ScriptableObject
{
    public List<FruitData> fruitDataList;

    public FruitData GetFruitData(FruitType type)
    {
        return fruitDataList.Find(fruit => fruit.type == type);
    }
}