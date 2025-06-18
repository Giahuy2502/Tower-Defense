using _Asset.Scripts.MyAsset;
using UnityEngine;
using UnityEngine.AI;

public class BaseMonster : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float hp = 100f; 
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float despawnDelay = 5f; // Thêm config cho delay
    [SerializeField] private Transform targetPos;
    [Header("AI Navigate")]
    [SerializeField] protected Transform target; 
    [SerializeField] protected NavMeshAgent agent;
    protected Animator animator;
    protected MonsterState currentState = MonsterState.Normal;
    protected MapManager mapManager => MapManager.instance;

    public MonsterState CurrentState => currentState;
    public float CurrentHp => hp;
    public Transform TargetPos
    {
        get => targetPos;
        set => targetPos = value;
    }
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = mapManager.EndPos;
        agent.speed = speed;
    }

    protected virtual void Update()
    {
        if(currentState == MonsterState.Die) return;
        
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        if (IsOnTarget())
        {
            ReachTarget();
        }
    }

    private bool IsOnTarget()
    {
        var currentPos = transform.position;
        currentPos.y = target.position.y;
        return Vector3.Distance(currentPos, target.position) < 0.1f;
    }

    protected virtual void ReachTarget()
    {
        RemoveFromManager();
        Destroy(gameObject);
    }
    
    public virtual void TakeDamage(float damage)
    {
        if (currentState == MonsterState.Die) return;
        
        hp -= damage;
        CheckCurrentHp();
    }

    protected virtual void CheckCurrentHp()
    {
        if (hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        currentState = MonsterState.Die;
        if (agent != null)
            agent.enabled = false;
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
        Destroy(gameObject);
    }
    protected virtual void OnDeath()
    {
        Debug.Log($"{gameObject.name} đã chết");
    }
}