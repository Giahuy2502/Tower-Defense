using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using static GameUltis;
public class UIEndGamePanel : MonoBehaviour
{
    [SerializeField] private UILosePanel losePanel;
    [SerializeField] private UIWinPanel winPanel;

    private void Awake()
    {
        EventSystem.Subscribe(EventName.WinGame,winPanel.WinGame);
        EventSystem.Subscribe(EventName.LoseGame,losePanel.LoseGame);
        Hide(losePanel.gameObject);
        Hide(winPanel.gameObject);
    }

    private void OnDestroy()
    {
        EventSystem.Unsubscribe(EventName.WinGame,winPanel.WinGame);
        EventSystem.Unsubscribe(EventName.LoseGame,losePanel.LoseGame);
    }
}
