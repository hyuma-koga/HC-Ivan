using UnityEngine;

[System.Serializable]
public class FruitSaveData
{
    public FruitType  type;        // フルーツの種類だけ保存
    public Vector3    position;    // 位置
    public Quaternion rotation;    // 回転
    public Vector3    scale;       // スケール
}
