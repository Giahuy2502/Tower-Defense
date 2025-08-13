using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public async Task SaveData()
    {
        TowerUnlockSaveData saveData = TowerUnlock.instance.GetSaveData();
        int userGem = EconomySystem.instance.Gem;
        string unlockedTowerJson = JsonUtility.ToJson(saveData);
        PlayerPrefs.DeleteKey("towerUnlock");
        PlayerPrefs.DeleteKey("userGem");
        PlayerPrefs.SetString("towerUnlock", unlockedTowerJson);
        PlayerPrefs.SetInt("userGem", userGem);
        PlayerPrefs.Save();
        Debug.Log(unlockedTowerJson);
    }
    public async Task LoadData()
    {
        await Task.Yield();
        int userGem = PlayerPrefs.GetInt("userGem");
        EconomySystem.instance.LoadData(userGem);

        string unlockedTowerJson = PlayerPrefs.GetString("towerUnlock");
        if (!string.IsNullOrEmpty(unlockedTowerJson))
        {
            TowerUnlockSaveData saveData = JsonUtility.FromJson<TowerUnlockSaveData>(unlockedTowerJson);
            Debug.Log(saveData.towers.Count);
            if (saveData.towers.Count > 0)
            {
                TowerUnlock.instance.LoadFromData(saveData);
            }
        }
    }


}
