using System;
using _Asset.Scripts.MyAsset;
using UnityEngine;

[RequireComponent(typeof(TowerAnimation))]
public class TowerAttack : MonoBehaviour
{
    [Header("Tower Attack Settings")]
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenBullets = 1f;
    [SerializeField] private float damage;
    [SerializeField] private float range = 15f;
    private TowerAnimation anim;
    private BaseTower baseTower;
    private float fireCountdown;
    private float rotateSpeed;
    private GameObject targetMonster;

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    private void Awake()
    {
        baseTower = GetComponent<BaseTower>();
        anim = GetComponent<TowerAnimation>();
    }

    private void Start()
    {
        range = baseTower.BaseTowerRange;
        rotateSpeed = baseTower.RotateSpeed;
        damage = baseTower.BaseTowerDamage;
    }

    private void Update()
    {
        UpdateTargetMonster();

        if (targetMonster == null)
        {
            fireCountdown = 0f;
            return;
        }

        RotateBarrelToTarget();
        if (!IsBarrelFacingTarget()) return;
        fireCountdown -= Time.deltaTime;
        if (fireCountdown <= 0f)
        {
            Attack();
            fireCountdown = timeBetweenBullets;
        }
    }

    private void UpdateTargetMonster()
    {
        if (targetMonster == null || 
            !targetMonster.activeSelf || 
            Vector3.Distance(transform.position, targetMonster.transform.position) > range)
        {
            targetMonster = FindMonsterInRange();
            return;
        }

        if (targetMonster != null)
        {
            var baseMonster = targetMonster.GetComponent<BaseMonster>();
            if(baseMonster == null || baseMonster.CurrentState == MonsterState.Die) targetMonster = FindMonsterInRange();
        }
    }

    private GameObject FindMonsterInRange()
    {
        var colliders = Physics.OverlapSphere(transform.position, range);
        foreach (var collider in colliders)
        {
            var monster = collider.GetComponent<BaseMonster>();
            if (monster != null && monster.CurrentState == MonsterState.Normal)
            {
                return monster.gameObject;
            }
        }
        return null;
    }

    private void Attack()
    {
        anim.SetAttack();
    }
    private void SpawnBullet() // this is a animation event
    {
        if (targetMonster == null) return;

        var newBullet = BulletPool.instance.GetObjectFromPool(TowerType.Runeblast, barrel.transform.position, barrel.transform.rotation);
        newBullet.transform.rotation = barrel.transform.rotation;

        var bullet = newBullet.GetComponent<BaseBullet>();
        bullet.Damage = this.damage;
        bullet.TargetMonster = this.targetMonster;
    }

    private void RotateBarrelToTarget()
    {
        if (barrel == null || targetMonster == null) return;

        Vector3 direction = (targetMonster.transform.position - barrel.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    
    private bool IsBarrelFacingTarget()
    {
        if (barrel == null || targetMonster == null) return false;

        Vector3 directionToTarget = (targetMonster.transform.position - barrel.transform.position).normalized;
        float angle = Vector3.Angle(barrel.transform.forward, directionToTarget);

        return angle < 5f;
    }

}
