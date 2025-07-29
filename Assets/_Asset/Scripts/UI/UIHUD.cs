using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using TMPro;
using UnityEngine;

public class UIHUD : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI monstersText;
    private EconomySystem economySystem => EconomySystem.instance;
    private MapManager mapManager => MapManager.instance;
    private void Awake()
    {
        EventSystem.Subscribe(EventName.UpdateGoldTxt,UpdateGoldTxt);
        EventSystem.Subscribe(EventName.UpdateMonsterTxt,UpdateMonsterTxt);
    }

    private void UpdateGoldTxt()
    {
        goldText.text = economySystem.Gold.ToString();
    }

    private void UpdateMonsterTxt()
    {
        monstersText.text = mapManager.MonsterCount.ToString();
    }
    private void OnDestroy()
    {
        EventSystem.Unsubscribe(EventName.UpdateGoldTxt,UpdateGoldTxt);
        EventSystem.Unsubscribe(EventName.UpdateMonsterTxt,UpdateMonsterTxt);
    }
}
