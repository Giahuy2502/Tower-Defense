using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUltis;
public class UIProduct : MonoBehaviour
{

    [SerializeField] private GameObject locked;
    [SerializeField] private int towerIndex;
    [SerializeField] private bool isUnlocked;
    [SerializeField] private GameObject unlockIcon;
    public int TowerIndex
    {
        get => towerIndex;
        set => towerIndex = value;
    }

    private void Awake()
    {
        SetLock();
    }

    void SetLock()
    {
        if(isUnlocked) Unlock();
        else Lock();
    }
    public void Lock()
    {
        isUnlocked = false;
        Hide(unlockIcon);
        Show(locked);
    }
    public void Unlock()
    {
        isUnlocked = true;
        Show(unlockIcon);
        Hide(locked);
    }
}
