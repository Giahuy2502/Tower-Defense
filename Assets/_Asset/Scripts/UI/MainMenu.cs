using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUltis;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startbtn;
    [SerializeField] private Button exitbtn;
    [SerializeField] private Button settingbtn;

    [SerializeField] private GameObject settingPanel;
    private GameManager gameManager => GameManager.instance;

    private void Awake()
    {
        startbtn.onClick.AddListener(OnStartGame);
        exitbtn.onClick.AddListener(OnExitGame);
        settingbtn.onClick.AddListener(OnSetting);
        Hide(settingPanel);
    }

    private void OnStartGame()
    {
        // Debug.Log("Call StartGame");
        gameManager.Play();
    }

    private void OnExitGame()
    {
        // Debug.Log("Call ExitGame");
        gameManager.ExitGame();
    }

    private void OnSetting()
    {
        Show(settingPanel);
    }
}
