using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    [SerializeField] private LevelDatabase levelData;
    [SerializeField] private int currentLevel = 1;

    public int CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        EventSystem.Subscribe(EventName.NextLevel, OnNextLevel);
    }

    public void OnDestroy()
    {
        
    }

    public LevelConfig GetCurrentLevelConfig()
    {
        return levelData.LevelList[currentLevel-1];
    }

    public LevelConfig GetLevelConfig(int level)
    {
        return levelData.LevelList[level-1];
    }

    public void OnNextLevel()
    {
        currentLevel++;
    }
}
