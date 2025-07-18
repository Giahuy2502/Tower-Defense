using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetAttack()
    {
        SetTrigger("IsAttack");
    }

    public void SetTowerDisappear()
    {
        
    }

    public void SetTowerAppear()
    {
        
    }
    private void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    private void SetFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }

    private void SetInt(string name, int value)
    {
        animator.SetInteger(name, value);
    }

    private void SetTrigger(string name)
    {
        animator.SetTrigger(name);
    }
}
