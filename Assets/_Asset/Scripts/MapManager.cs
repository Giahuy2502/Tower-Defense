using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    [Header("Game Stats")]
    [SerializeField] private int monsterCount;
    [SerializeField] private int maxMonsterCount;
    [SerializeField] private List<GameObject> monsterPrefabs;
    [SerializeField] private List<GameObject> activeMonsters;
    [Header("AI Navigation")]
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    

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

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        monsterCount = 0;
        SpawnMonsters();
    }

    
    void Update()
    {
        if (activeMonsters.Count < maxMonsterCount)
        {
            Debug.Log($"monsterCount: {monsterCount} / maxMonsterCount: {maxMonsterCount}");
            SpawnMonsters();
        }
        
    }

    private void SpawnMonsters()
    {
        var newMonster = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Count)], startPos.position, Quaternion.identity);
        activeMonsters.Add(newMonster);
        monsterCount++;
    }
    
}
