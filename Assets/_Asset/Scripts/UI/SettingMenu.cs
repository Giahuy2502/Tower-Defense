using System.Collections;
using System.Collections.Generic;
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
    
    private GameManager gameManager => GameManager.instance;

    private void Awake()
    {
        settingBtn.onClick.AddListener(OnSetting);
        closeBtn.onClick.AddListener(OnClose);
        Hide(settingPanel);
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
}
