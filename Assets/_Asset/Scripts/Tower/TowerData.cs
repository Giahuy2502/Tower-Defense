using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "TowerDefense/TowerData",fileName = "TowerData")]
public class TowerData : ScriptableObject
{
    [SerializeField] private List<Tower> towers = new  ();
    public  List<Tower> Towers
    {
        get => towers;
        set => towers = value;
    }
    public Dictionary<TowerType,List<TowerInfo>> towerLevelUps = new ();

    private void OnValidate()
    {
        foreach (var tower in towers)
        {
            towerLevelUps[tower.towerType] = tower.towers;
            foreach (var towerInfo in tower.towers)
            {
                if (towerInfo.towerPrefab != null)
                {
                    towerInfo.towerName = towerInfo.towerPrefab.name;
                }
            }
        }
    }
}

[System.Serializable]
public class Tower
{
    public TowerType towerType;
    public List<TowerInfo> towers = new List<TowerInfo>();
}
[System.Serializable]
public class TowerInfo
{
    public string towerName;
    public int level;
    public int damage;
    public float range;
    public int upgradeCost;
    public float upgradeTime; // seconds
    public GameObject towerPrefab;
}