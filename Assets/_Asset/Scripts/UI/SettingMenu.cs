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
    [SerializeField] private GameObject buildTowerPanel;
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
    private GameManager gameManager => GameManager.instance;

    private void Awake()
    {
        settingBtn.onClick.AddListener(OnSetting);
        closeBtn.onClick.AddListener(OnClose);
        tutorialBtn.onClick.AddListener(OnTutorial);
        logoutBtn.onClick.AddListener(OnLogout);
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);
        Hide(settingPanel);
    }

    private void OnMusicSliderChanged(float value)
    {
        musicIcon.sprite = value <= 0 ? musicIconOff : musicIconOn;
    }

    private void OnSfxSliderChanged(float value)
    {
        soundIcon.sprite = value <= 0 ? soundIconOff : soundIconOn;
    }
    private void OnSetting()
    {
        Debug.Log("Pause Panel OnPause");
        Show(settingPanel);
        Hide(buildTowerPanel);
    }

    private void OnClose()
    {
        Hide(settingPanel);
        Show(buildTowerPanel);
    }

    private void OnTutorial()
    {
        Debug.Log("Show Tutorial Panel");
        Hide(settingPanel);
        Show(buildTowerPanel);
    }
    private void OnLogout()
    {
        Debug.Log("Logout game");
        Hide(settingPanel);
        Show(buildTowerPanel);
    }
    
}
