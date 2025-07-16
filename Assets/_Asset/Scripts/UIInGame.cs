using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using TMPro;
using UnityEngine;

public class UIInGame : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI goldText;
    private EconomySystem economySystem => EconomySystem.instance;

    private void Awake()
    {
        EventSystem.Subscribe(EventName.UpdateGoldTxt,UpdateGoldTxt);
    }

    public void UpdateGoldTxt()
    {
        goldText.text = "Gold: " + economySystem.Gold;
    }
}
