using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;

[CreateAssetMenu(menuName = "TowerDefense/TowerCost",fileName = "TowerCost")]
public class TowerCost : ScriptableObject
{
    [SerializeField] private List<TowerCostData> towerCostDatas;

    private Dictionary<TowerType, int> towerCostMap;
    private void OnValidate()
    {
        towerCostMap = new Dictionary<TowerType, int>();
        foreach (var cost in towerCostDatas)
        {
            if (!towerCostMap.ContainsKey(cost.towerType))
                towerCostMap[cost.towerType] = cost.cost;
        }
    }
    public int GetCost(TowerType type)
    {
        if (towerCostMap.TryGetValue(type, out var cost))
            return cost;

        Debug.LogError($"Tower cost not found for {type}");
        return -1;
    }
    public List<TowerCostData> TowerCostDatas
    {
        get => towerCostDatas;
        set => towerCostDatas = value;
    }
}

[Serializable]
public class TowerCostData
{
    public TowerType towerType;
    public int cost;
}