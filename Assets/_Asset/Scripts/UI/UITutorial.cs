using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUltis;
public class UITutorial : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private GameManager gameManager => GameManager.instance;
    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnClose);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
    }

    private void OnClose()
    {
        gameManager.ResumeGame();
        Hide(gameObject);
    }
    
}
