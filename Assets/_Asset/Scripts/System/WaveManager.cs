using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int currentWave ;
    [SerializeField] private LevelConfig currentLevel;

    [SerializeField] private int numberOfAvailabeMonsters = 0;
    public static WaveManager instance;
    private MapManager mapManager => MapManager.instance;
    private MonsterPool monsterPool => MonsterPool.instance;
    private EconomySystem economySystem => EconomySystem.instance;
    private LevelController levelController => LevelController.instance;

    public int NumberOfAvailabeMonsters
    {
        get => numberOfAvailabeMonsters;
        set => numberOfAvailabeMonsters = value;
    }

    public LevelConfig CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    public int CurrentWave
    {
        get => currentWave;
        set => currentWave = value;
    }

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
        EventSystem.Subscribe(EventName.StartGame,GetData);
        EventSystem.Subscribe(EventName.StartGame,economySystem.StartGame);
    }

    private void OnDestroy()
    {
        EventSystem.Unsubscribe(EventName.StartGame,GetData);
        EventSystem.Unsubscribe(EventName.StartGame,economySystem.StartGame);
    }

    private void GetData()
    {
        currentLevel = levelController.GetCurrentLevelConfig();
        numberOfAvailabeMonsters = GetTotalMonstersInLevel();
    }
    IEnumerator StartWave(EnemyWave wave)
    {
        EventSystem.Invoke(EventName.UpdateWaveProcessTxt);
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
        var monsterData = monster.monsterData;
        if(count <=0 ) yield break;
        while (count > 0)
        {
            monsterPool.GetObjectFromPool(monsterData.monsterType,mapManager.StartPos.position,Quaternion.identity,monster);
            count--;
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        if (mapManager.ActiveMonsterCount <= 0 && WaveCoroutine == null && currentWave < currentLevel.enemyWaves.Count)
        {
            WaveCoroutine = StartCoroutine(StartWave(currentLevel.enemyWaves[currentWave]));
            currentWave++;
        }
    }

    private int GetTotalMonstersInLevel()
    {
        var totalMonster = 0;
        foreach (var enemyWave in currentLevel.enemyWaves)
        {
            foreach (var enemySpawnInfo in enemyWave.enemySpawns)
            {
                totalMonster += enemySpawnInfo.spawnCount;
            }
        }
        return totalMonster;
    }
    
}
