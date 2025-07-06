using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveMonster : MonoBehaviour
{
    [FormerlySerializedAs("target")]
    [Header("AI Navigate")]
    [SerializeField] protected List<Vector3> wayTransforms;
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
        foreach (var point in mapManager.Waypoints)
        {
            wayTransforms.Add(point.transform.position);
        }
    }

    private void OnEnable()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }
        transform.position = mapManager.StartPos.position;
        if (wayTransforms != null)
        {
            Move();
        }
    }

    public void Move()
    {
        if (wayTransforms == null) return;

        Vector3[] waypoints = wayTransforms.ToArray();
        var nextWay = mapManager.EndPos.position;
        float distance = Vector3.Distance(transform.position, nextWay);
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
        Debug.Log("Monster reached target");
    }
   

}
