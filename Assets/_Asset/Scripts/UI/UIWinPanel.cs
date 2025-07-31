using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameUltis;

public class UIWinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button nextButton;

    private GameManager gameManager => GameManager.instance;
    private WaveManager waveManager => WaveManager.instance;
    
    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestart);
        homeButton.onClick.AddListener(OnHome);
        nextButton.onClick.AddListener(OnNext);
    }

    public void WinGame()
    {
        Show(gameObject);
        UpdateRewardText();
    }

    private void UpdateRewardText()
    {
        var rewards = waveManager.CurrentLevel.rewards;
        var golds = rewards.rewardGold;
        rewardText.text = "x" + golds.ToString();
    }

    private void OnRestart()
    {
        gameManager.RestartGame();
    }

    private void OnHome()
    {
        gameManager.OnHome();
    }

    private void OnNext()
    {
        gameManager.RestartGame();
    }
}
