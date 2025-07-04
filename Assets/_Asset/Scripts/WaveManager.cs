using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int currentWave ;
    [SerializeField] private LevelData levelData;
    [SerializeField] private Level currentLevel;
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
        currentLevel = levelData.Levels[level-1];
    }
    IEnumerator StartWave(Wave wave)
    {
        foreach (var monster in wave.monsters)
        {
            yield return StartCoroutine(SpawnMonster(monster));
            yield return new WaitForSeconds(1.5f);
        }

        WaveCoroutine = null;
    }
    
    IEnumerator SpawnMonster(Monster monster)
    {
        var count = monster.count;
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
        if (mapManager.MonsterCount <= 0 && WaveCoroutine == null && currentWave < currentLevel.waves.Count)
        {
            // Debug.Log($"Waves : {currentWave}");
            WaveCoroutine = StartCoroutine(StartWave(currentLevel.waves[currentWave]));
            currentWave++;
        }
    }

}
