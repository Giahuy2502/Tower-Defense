using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startbtn;
    [SerializeField] private Button exitbtn;

    private GameManager gameManager => GameManager.instance;

    private void Awake()
    {
        startbtn.onClick.AddListener(OnStartGame);
        exitbtn.onClick.AddListener(OnExitGame);
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
}
