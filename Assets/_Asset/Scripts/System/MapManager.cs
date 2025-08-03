using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using static GameUltis;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    [SerializeField] private int activeMonsterCount;
    [Header("Game Stats")] 
    public int level;
    [SerializeField] private List<GameObject> activeMonsters;
    [SerializeField] private int monstersReachedCount;
    [SerializeField] private int defeatedMonsterCount;
    [SerializeField] private int maxMonstersReached;
    [Header("AI Navigation")]
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private List<GameObject> waypoints;
    [Header("Map")]
    [SerializeField] private Grid grid;
    [SerializeField] private List<MapTag> mapTags = new List<MapTag>();
    [SerializeField] private List<GameObject> mapObjects;
    private GridData gridData = new();
    private WaveManager waveManager => WaveManager.instance;
    private GameManager gameManager => GameManager.instance;
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

    public int ActiveMonsterCount
    {
        get => activeMonsterCount;
        set => activeMonsterCount = value;
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

    public int MonstersReachedCount
    {
        get => monstersReachedCount;
        set => monstersReachedCount = value;
    }

    public int DefeatedMonsterCount
    {
        get => defeatedMonsterCount;
        set => defeatedMonsterCount = value;
    }

    public int MaxMonstersReached
    {
        get => maxMonstersReached;
        set => maxMonstersReached = value;
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


    private void Start()
    {
        SceneLoadingManager.OnLoadingComplete += gameManager.StartGame;
    }

    private void OnDestroy()
    {
        SceneLoadingManager.OnLoadingComplete -= gameManager.StartGame;
    }

    void Update()
    {
        if(gameManager == null) Debug.Log("Game Manager is null");
        if(waveManager == null) Debug.Log("Wave Manager is null");
    }

    public void RemoveFromManager(GameObject monster)
    {
        activeMonsterCount--;
        waveManager.NumberOfAvailabeMonsters--;
        ActiveMonsters.Remove(monster);
        if (waveManager.NumberOfAvailabeMonsters <= 0)
        {
            gameManager.WinGame();
        }
    }

    public void UpdateMonsterReachedCount(GameObject monster)
    {
        monstersReachedCount+=1;
        if (monstersReachedCount >= maxMonstersReached)
        {
            gameManager.LoseGame();
            return;
        }
        RemoveFromManager(monster);
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
