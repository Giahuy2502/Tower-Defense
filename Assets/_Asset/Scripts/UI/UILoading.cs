using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float loadTime = 2.5f;
    private void Awake()
    {
        slider.value = 0.075f;
        slider.maxValue = loadTime;
        slider.onValueChanged.AddListener(CheckValue);
    }

    private void Update()
    {
        slider.value += Time.deltaTime;
    }

    private void CheckValue(float value)
    {
        if (value >= slider.maxValue)
        {
            SceneManager.UnloadSceneAsync(this.gameObject.scene);
            SceneLoadingManager.NotifyLoadingComplete();
        }
    }
}