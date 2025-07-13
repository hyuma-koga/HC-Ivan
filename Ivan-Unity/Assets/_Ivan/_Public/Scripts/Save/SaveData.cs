using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<FruitData> activeFruits;
    public FruitData       standbyFruit;
    public FruitData       nextFruit;
    public int　　　　　　 score;
}