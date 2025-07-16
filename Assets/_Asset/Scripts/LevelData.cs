using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "TowerDefense/Level Database")]
public class LevelDatabase : ScriptableObject
{
    [SerializeField] private List<LevelConfig> levelList = new();

    public List<LevelConfig> LevelList
    {
        get => levelList;
        set => levelList = value;
    }
}

[System.Serializable]
public class LevelConfig
{
    public int levelNumber;                   
    public LevelRewardConfig rewards;       
    public List<EnemyWave> enemyWaves = new(); 
}

[System.Serializable]
public class EnemyWave
{
    public List<EnemySpawnInfo> enemySpawns = new(); 
}
[System.Serializable]
public class EnemySpawnInfo
{
    public int spawnCount;               
    public MonsterData monsterData;    
    public int rewardGold;              
}


[System.Serializable]
public class LevelRewardConfig
{
    [Header("Starting Resources")]
    public int startingGold;              
    [Header("Completion Rewards")]
    public int rewardGold;                 
    public int rewardGems;            
    public int rewardExp;      
}