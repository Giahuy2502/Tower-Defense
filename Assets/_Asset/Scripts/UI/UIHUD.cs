using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using TMPro;
using UnityEngine;

public class UIHUD : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI waveProcesstext;
    private EconomySystem economySystem => EconomySystem.instance;
    private WaveManager waveManager => WaveManager.instance;
    private MapManager mapManager => MapManager.instance;
    private void Awake()
    {
        EventSystem.Subscribe(EventName.UpdateGoldTxt,UpdateGoldTxt);
        EventSystem.Subscribe(EventName.UpdateLivesTxt,UpdateLivesText);
        EventSystem.Subscribe(EventName.UpdateWaveProcessTxt,UpdateWavesProcessText);
        EventSystem.Subscribe(EventName.UpdateGemText,UpdateGemText);
    }

    private void Start()
    {
        UpdateLivesText();
        UpdateGoldTxt();
        UpdateWavesProcessText();
        UpdateGemText();
    }

    private void UpdateGoldTxt()
    {
        goldText.text = economySystem.Gold.ToString();
    }

    void UpdateGemText()
    {
        gemText.text = economySystem.Gem.ToString();
    }

    private void UpdateLivesText()
    {
        var maxLive = mapManager.MaxMonstersReached;
        var currentLive = mapManager.MaxMonstersReached - mapManager.MonstersReachedCount;
        livesText.text = currentLive + "/" + maxLive;
    }

    private void UpdateWavesProcessText()
    {
        var maxWaveCount = waveManager.CurrentLevel.enemyWaves.Count;
        var currentWave = waveManager.CurrentWave + 1;
        waveProcesstext.text = currentWave + "/" + maxWaveCount;
    }
    private void OnDestroy()
    {
        EventSystem.Unsubscribe(EventName.UpdateGoldTxt,UpdateGoldTxt);
        EventSystem.Unsubscribe(EventName.UpdateLivesTxt,UpdateLivesText);
        EventSystem.Unsubscribe(EventName.UpdateWaveProcessTxt,UpdateWavesProcessText);
        EventSystem.Unsubscribe(EventName.UpdateGemText,UpdateGemText);
    }
}
