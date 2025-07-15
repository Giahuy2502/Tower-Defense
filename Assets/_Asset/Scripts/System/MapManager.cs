using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using Random = UnityEngine.Random;
using static GameUltis;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    [Header("Game Stats")] public int level;
    [SerializeField] private int monsterCount;
    [SerializeField] private int maxMonsterCount;
    [SerializeField] private List<GameObject> monsterPrefabs;
    [SerializeField] private List<GameObject> activeMonsters;
    [Header("AI Navigation")]
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private List<GameObject> waypoints;
    [Header("Map")]
    [SerializeField] private Grid grid;
    [SerializeField] private List<MapTag> mapTags = new List<MapTag>();
    [SerializeField] private List<GameObject> mapObjects;
    private GridData gridData = new();
    public Transform StartPos
    {
        get => startPos;
        set => startPos = value;
    }

    public Transform EndPos
    {
        get => endPos;
        set => endPos = value;
    }

    public int MonsterCount
    {
        get => monsterCount;
        set => monsterCount = value;
    }

    public List<GameObject> ActiveMonsters
    {
        get => activeMonsters;
        set => activeMonsters = value;
    }

    public List<GameObject> Waypoints
    {
        get => waypoints;
        set => waypoints = value;
    }

    public GridData GridData
    {
        get => gridData;
        set => gridData = value;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SetGridData();
    }

    void Start()
    {
        monsterCount = 0;
    }

    public void RemoveFromManager(GameObject monster)
    {
        monsterCount--;
        ActiveMonsters.Remove(monster);
    }

    private void SetGridData()
    {
        mapObjects.Clear();

        foreach (var tag in mapTags)
        {
            var objects = GameObject.FindGameObjectsWithTag(tag.ToString());
            mapObjects.AddRange(objects);
        }

        foreach (var obj in mapObjects)
        {
            Vector2Int objSize = GetSize(obj);
            Vector3Int gridPos = GetCellPositionInt(grid,obj.transform.position);
            gridData.AddObjectAt(gridPos,objSize, obj);
        }
    }
}
