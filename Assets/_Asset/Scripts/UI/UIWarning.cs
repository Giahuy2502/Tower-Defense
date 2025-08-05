using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameUltis;
public class UIWarning : MonoBehaviour
{
    [SerializeField] private Button no;
    [SerializeField] private Button yes;

    private void Awake()
    {
        no.onClick.AddListener(OnNo);
        yes.onClick.AddListener(OnYes);
    }

    private void OnNo()
    {
        Hide(gameObject);
    }

    private void OnYes()
    {
        Hide(gameObject);
    }
}
