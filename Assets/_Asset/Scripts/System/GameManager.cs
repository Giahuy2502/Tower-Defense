using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnHome()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
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
}
