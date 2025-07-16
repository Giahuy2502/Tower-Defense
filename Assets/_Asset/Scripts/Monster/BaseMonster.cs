using System;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(HealthMonster),(typeof(MoveMonster)))]
public class BaseMonster : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected HealthMonster healthMonster;
    [SerializeField] private MoveMonster moveMonster;
    public float speed = 1f;
    [SerializeField] protected float despawnDelay = 5f;
    [SerializeField] private Transform targetPos;
    [SerializeField] private float rewardGold;
    
    protected Animator animator;
    protected MonsterState currentState = MonsterState.Normal;
    protected MapManager mapManager => MapManager.instance;

    

    public MonsterState CurrentState => currentState;

    public Transform TargetPos
    {
        get => targetPos;
        set => targetPos = value;
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        moveMonster = GetComponent<MoveMonster>();
        moveMonster.MoveSpeed = speed;
    }

    public void OnEnable()
    {
        currentState = MonsterState.Normal;
        healthMonster = GetComponent<HealthMonster>();
        
    }

    public void Initialize(EnemySpawnInfo monsterSpawnInfo)
    {
        var monsterData = monsterSpawnInfo.monsterData;
        healthMonster.HealthMax = monsterData.monsterHealth;
        moveMonster.MoveSpeed = monsterData.monsterSpeed;
        rewardGold = monsterSpawnInfo.rewardGold;
    }

    protected virtual void Update()
    {
        if (currentState == MonsterState.Die) return;
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

        moveMonster.StopMove();

        PlayDeathAnimation();
        mapManager.RemoveFromManager(gameObject);
        Invoke(nameof(Despawn), despawnDelay);
    }

    protected virtual void PlayDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Die", true);
        }
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
