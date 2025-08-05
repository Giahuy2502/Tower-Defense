using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using TMPro;
using UnityEngine;

public class UIMainHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemText;

    private EconomySystem economySystem => EconomySystem.instance;
    private void Awake()
    {
        EventSystem.Subscribe(EventName.UpdateGemText,UpdateGemText);
    }

    private void OnDestroy()
    {
        EventSystem.Unsubscribe(EventName.UpdateGemText,UpdateGemText);
    }

    private void Start()
    {  
        UpdateGemText();
    }
    private void UpdateGemText()
    {
        gemText.text = economySystem.Gem.ToString();
    }
}
