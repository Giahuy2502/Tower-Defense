using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using static GameUltis;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [Header("Pause Panel")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button exitButton;

    private GameManager gameManager => GameManager.instance;

    private void Awake()
    {
        pauseButton.onClick.AddListener(OnPause);
        homeButton.onClick.AddListener(OnHome);
        exitButton.onClick.AddListener(OnExit);
        resumeButton.onClick.AddListener(OnResume);
        Hide(pausePanel);
    }

    private void OnPause()
    {
        Debug.Log("Pause Panel OnPause");
        Show(pausePanel);
        gameManager.PauseGame();
    }
    private void OnHome()
    {
        gameManager.ResumeGame();
        gameManager.OnHome();
    }

    private void OnResume()
    {
        gameManager.ResumeGame();
        Hide(pausePanel);
    }

    private void OnExit()
    {
        gameManager.ExitGame();
    }
}
