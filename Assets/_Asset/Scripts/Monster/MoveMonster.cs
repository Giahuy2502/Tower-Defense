using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveMonster : MonoBehaviour
{
    [Header("AI Navigate")]
    [SerializeField] protected Transform target;
    [SerializeField] private float moveSpeed;
    private Tween moveTween;
    protected MapManager mapManager => MapManager.instance;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    private void Awake()
    {
        target = mapManager.EndPos;
    }

    private void OnEnable()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }
        transform.position = mapManager.StartPos.position;
        if (target != null)
        {
            Move();
        }
    }

    public void Move()
    {
        if (target == null) return;

        Vector3[] waypoints = new Vector3[] { target.position };
        
        float distance = Vector3.Distance(transform.position, target.position);
        float duration = distance / moveSpeed;
        moveTween = transform.DOPath(waypoints, duration, PathType.Linear, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .SetLookAt(0.1f)
            .OnComplete(() =>
            {
                ReachTarget();
            });
    }

    public void StopMove()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }
    }
    public virtual void ReachTarget()
    {
        mapManager.RemoveFromManager(gameObject);
        MonsterPool.instance.ReturnObjectToPool(gameObject);
    }
   

}
