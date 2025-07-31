using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using static GameUltis;
public class UILosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldCollectedText;
    [SerializeField] private TextMeshProUGUI monsterDefeatedText;
    
    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;
    
    private GameManager gameManager => GameManager.instance;
    private MapManager mapManager => MapManager.instance;
    private EconomySystem economySystem => EconomySystem.instance;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestart);
        homeButton.onClick.AddListener(OnHome);
    }

    public void LoseGame()
    {
        Show(gameObject);
        UpdateGoldCollectedText();
        UpdateMonsterDefeatedText();
    }

    private void UpdateGoldCollectedText()
    {
        goldCollectedText.text = "Gold collected: "+economySystem.Gold.ToString();
    }

    private void UpdateMonsterDefeatedText()
    {
        monsterDefeatedText.text ="Monsters Defeated: "+ mapManager.DefeatedMonsterCount.ToString();
    }

    private void OnRestart()
    {
        gameManager.RestartGame();
    }   

    private void OnHome()
    {
        gameManager.OnHome();
    }

}
