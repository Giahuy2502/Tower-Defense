using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int currentWave ;
    [SerializeField] private LevelDatabase levelData;
    [SerializeField] private LevelConfig currentLevel;
    public static WaveManager instance;
    private MapManager mapManager => MapManager.instance;
    private MonsterPool monsterPool => MonsterPool.instance;
    private Coroutine WaveCoroutine;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        GetData();
    }
    private void GetData()
    {
        level = mapManager.level;
        currentLevel = levelData.LevelList[level-1];
    }
    IEnumerator StartWave(EnemyWave wave)
    {
        foreach (var monster in wave.enemySpawns)
        {
            yield return StartCoroutine(SpawnMonster(monster));
            yield return new WaitForSeconds(1.5f);
        }

        WaveCoroutine = null;
    }
    
    IEnumerator SpawnMonster(EnemySpawnInfo monster)
    {
        var count = monster.spawnCount;
        // Debug.Log($"Spawning monster Count {count}");
        var monsterData = monster.monsterData;
        if(count <=0 ) yield break;
        while (count > 0)
        {
            monsterPool.GetObjectFromPool(monsterData.monsterType,mapManager.StartPos.position,Quaternion.identity);
            count--;
            // Debug.Log($"Spawning monster {monsterData.monsterType} {count}");
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        if (mapManager.MonsterCount <= 0 && WaveCoroutine == null && currentWave < currentLevel.enemyWaves.Count)
        {
            // Debug.Log($"Waves : {currentWave}");
            WaveCoroutine = StartCoroutine(StartWave(currentLevel.enemyWaves[currentWave]));
            currentWave++;
        }
    }

}
