using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<FruitSaveData> activeFruits;
    public FruitSaveData standbyFruit;
    public FruitType nextFruitType;
    public int score;
}