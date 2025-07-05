using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using Random = UnityEngine.Random;

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
    }

    public void RemoveFromManager(GameObject monster)
    {
        monsterCount--;
        ActiveMonsters.Remove(monster);
    }
}
