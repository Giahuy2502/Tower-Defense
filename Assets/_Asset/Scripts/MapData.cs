using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapData", menuName = "TowerDefense/MapData")]
public class MapData : ScriptableObject
{
    [SerializeField]private List<Map> maps = new List<Map>();
    public List<Map> Maps { get => maps; private set => maps = value; }

    public Map Getmap(int index)
    {
        return maps[index];
    }
}

[Serializable]
public class Map
{
    public int level;
    public GameObject mapPrefab;
}
