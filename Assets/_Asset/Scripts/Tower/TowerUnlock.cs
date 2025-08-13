using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using Unity.VisualScripting;
using UnityEngine;

public class TowerUnlock : MonoBehaviour
{
    [SerializeField] private List<TowerUnlockData> data = new ();
    public Dictionary<TowerType,TowerUnlockData> dataUnlockedMap = new ();
    public static TowerUnlock instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnValidate()
    {
        dataUnlockedMap = new Dictionary<TowerType,TowerUnlockData>();
        foreach (TowerUnlockData data in data)
        {
            dataUnlockedMap[data.TowerType] = data;
        }
    }

    public void UnlockTowerType(TowerType towerType)
    {
        var data = dataUnlockedMap[towerType];
        data.isUnlocked = true;
    }

    [ContextMenu("Set Default")]
    public void SetDefault()
    {
        for (int i = 1; i < data.Count; i++)
        {
            data[i].isUnlocked = false;
        }
    }
}

[Serializable]
public class TowerUnlockData
{
    public TowerType TowerType;
    public bool isUnlocked;
}