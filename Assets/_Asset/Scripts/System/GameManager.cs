using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private UserData userData => UserData.instance;

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
    private async void Start()
    {
        await userData.LoadData();
        EventSystem.Invoke(EventName.UpdateGemText);
    }


    public void Play()
    {
        SceneManager.LoadScene(2);
        OnLoading();
    }

    public void OnHome()
    {
        SceneManager.LoadScene(1);
        OnLoading();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public async void ExitGame()
    {
        await userData.SaveData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        EventSystem.Invoke(EventName.WinGame);
    }

    public void LoseGame()
    {
        Debug.Log("You Lose!");
        EventSystem.Invoke(EventName.LoseGame);
    }

    public void RestartGame()
    {
        Debug.Log("You Restart!");
        EventSystem.Invoke(EventName.RestartGame);
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnLoading();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        EventSystem.Invoke(EventName.NextLevel);
        OnLoading();
    }

    public void StartGame()
    {
        EventSystem.Invoke(EventName.StartGame);
    }
    
    private void OnLoading()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        
    }
}
