using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeathBar : MonoBehaviour
{
    [SerializeField]private Slider slider;
    [SerializeField]private Gradient gradient;
    [SerializeField] private Image fill;
    public void SetMaxHeath(float heath)
    {
        slider.maxValue = heath;
        slider.value = heath;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetCurrentHeath(float heath)
    {
        slider.value = heath;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
