using System;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using DG.Tweening;

public class BaseMonster : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected HealthMonster healthMonster; 
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float despawnDelay = 5f;
    [SerializeField] private Transform targetPos;

    [Header("AI Navigate")]
    [SerializeField] protected Transform target;
    protected Animator animator;
    protected MonsterState currentState = MonsterState.Normal;
    protected MapManager mapManager => MapManager.instance;

    private Tween moveTween;

    public MonsterState CurrentState => currentState;

    public Transform TargetPos
    {
        get => targetPos;
        set => targetPos = value;
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        target = mapManager.EndPos;
    }

    public void OnEnable()
    {
        currentState = MonsterState.Normal;
        healthMonster = GetComponent<HealthMonster>();
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

    protected virtual void Move()
    {
        if (target == null) return;

        Vector3[] waypoints = new Vector3[] { target.position };
        
        float distance = Vector3.Distance(transform.position, target.position);
        float duration = distance / speed;
        moveTween = transform.DOPath(waypoints, duration, PathType.Linear, PathMode.Full3D)
            .SetEase(Ease.Linear)
            .SetLookAt(0.1f)
            .OnComplete(() =>
            {
                ReachTarget();
            });
    }

    protected virtual void Update()
    {
        if (currentState == MonsterState.Die) return;
    }

    protected virtual void ReachTarget()
    {
        RemoveFromManager();
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        if (currentState == MonsterState.Die) return;

        healthMonster.TakeDamage(damage);
        CheckCurrentHp();
    }

    protected virtual void CheckCurrentHp()
    {
        if (healthMonster.IsDead())
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        currentState = MonsterState.Die;

        // Dừng tween nếu đang chạy
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

        PlayDeathAnimation();
        RemoveFromManager();
        Invoke(nameof(Despawn), despawnDelay);
    }

    protected virtual void PlayDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Die", true);
        }
    }

    protected void RemoveFromManager()
    {
        mapManager.MonsterCount--;
        mapManager.ActiveMonsters.Remove(gameObject);
    }

    protected virtual void Despawn()
    {
        MonsterPool.instance.ReturnObjectToPool(gameObject);
    }

    protected virtual void OnDeath()
    {
        Debug.Log($"{gameObject.name} đã chết");
    }
}
