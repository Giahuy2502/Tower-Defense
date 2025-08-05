using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameUltis;
public class SettingMenu : MonoBehaviour
{
    [SerializeField] private Button settingBtn;
    [Header("Setting Panel")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button tutorialBtn;
    [SerializeField] private Button logoutBtn;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [Header("ICON")]
    [SerializeField] private Image soundIcon;
    [SerializeField] private Image musicIcon;
    [SerializeField] private Sprite soundIconOn;
    [SerializeField] private Sprite soundIconOff;
    [SerializeField] private Sprite musicIconOn;
    [SerializeField] private Sprite musicIconOff;
    [Header("Tutorial Panel")]
    [SerializeField] private GameObject tutorialMenu;
    private GameManager gameManager => GameManager.instance;
    private AudioManager audioManager => AudioManager.instance;

    private void Awake()
    {
        settingBtn.onClick.AddListener(OnSetting);
        closeBtn.onClick.AddListener(OnClose);
        tutorialBtn.onClick.AddListener(OnTutorial);
        logoutBtn.onClick.AddListener(OnLogout);
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);
    }

    private void OnMusicSliderChanged(float value)
    {
        musicIcon.sprite = value <= 0 ? musicIconOff : musicIconOn;
        audioManager.SetMusicVolume(value);
    }

    private void OnSfxSliderChanged(float value)
    {
        soundIcon.sprite = value <= 0 ? soundIconOff : soundIconOn;
        audioManager.SetSfxVolume(value);
    }
    private void OnSetting()
    {
        Debug.Log("Pause Panel OnPause");
        gameManager.PauseGame();
        Show(settingPanel);
    }

    private void OnClose()
    {
        gameManager.ResumeGame();
        Hide(settingPanel);
    }

    private void OnTutorial()
    {
        Debug.Log("Show Tutorial Panel");
        Show(tutorialMenu);
        Hide(settingPanel);
    }
    private void OnLogout()
    {
        Debug.Log("Logout game");
        gameManager.ResumeGame();
        Hide(settingPanel);
    }

    private void OnEnable()
    {
        
    }
}
