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
    [SerializeField] private List<GameObject> activeMonsters;
    [SerializeField] private int monstersReachedCount;
    [SerializeField] private int defeatedMonsterCount;
    [SerializeField] private int maxMonstersReached;
    [Header("AI Navigation")]
    [SerializeField] private MapData mapData;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private List<GameObject> waypoints;
    [SerializeField] private WayPointsManager wayPointsManager;
    [Header("Map")]
    [SerializeField] private Grid grid;
    [SerializeField] private List<MapTag> mapTags = new List<MapTag>();
    [SerializeField] private List<GameObject> mapObjects;
    private GridData gridData ;
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

    public Grid Grid
    {
        get => grid;
        set => grid = value;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.Log("---------Map Manager---------");
            return;
        }
        instance = this;
    }


    private void Start()
    {
        SceneLoadingManager.OnLoadingComplete += gameManager.StartGame;
        GetMap();
        SetGridData();
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
            // Debug.Log($"Set grid data for {obj.name} at {gridPos}");
        }
    }

    private void GetMap()
    {
        gridData = new GridData();
        var currentLevel = LevelController.instance.CurrentLevel;
        var map = mapData.Getmap(currentLevel-1);
        Instantiate(map.mapPrefab,Vector3.zero, Quaternion.identity);
        wayPointsManager = FindObjectOfType<WayPointsManager>();
        waypoints.Clear();
        waypoints = wayPointsManager.GetWayPoints();
        startPos = waypoints[0].transform;
        endPos = waypoints[waypoints.Count - 1].transform;
    }
}
