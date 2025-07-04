using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "TowerDefense/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private List<Level> levels = new List<Level>();

    public List<Level> Levels
    {
        get => levels;
        set => levels = value;
    }
}

[System.Serializable]
public class Level
{
    public int level;
    public List<Wave> waves = new();
}

[System.Serializable]
public class Wave
{
    public List<Monster> monsters = new();
}

[System.Serializable]
public class Monster
{
    public int count;
    public MonsterData monsterData;
}