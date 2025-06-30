using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TowerData",fileName = "TowerData")]
public class TowerData : ScriptableObject
{
    [SerializeField] private List<TowerLevelData> towers = new  List<TowerLevelData>();
    public  List<TowerLevelData> Towers
    {
        get => towers;
        set => towers = value;
    }
}
[System.Serializable]
public class TowerLevelData
{
    public string towerName;
    public int level;
    public int damage;
    public float range;
    public int upgradeCost;
    public float upgradeTime; // seconds
    public GameObject towerPrefab;
}