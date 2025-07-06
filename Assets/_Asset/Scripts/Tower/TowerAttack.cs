using System.Collections;
using _Asset.Scripts.MyAsset;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] protected GameObject barrel;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float timeBetweenBullets = 1f;
    [SerializeField] private float damage;
    public Coroutine attackCoroutine;

    public float Damage
    {
        get => damage;
        set => damage = value;
    }
    

    public void StartAttack(GameObject targetMonster)
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(Attack(targetMonster));
        }
    }

    private IEnumerator Attack(GameObject targetMonster)
    { 
        var monsterBase = targetMonster.GetComponent<BaseMonster>();
        while (targetMonster != null && monsterBase.CurrentState != MonsterState.Die && targetMonster.activeSelf)
        {
            AttackMonster(targetMonster);
            yield return new WaitForSeconds(timeBetweenBullets);
        }
        attackCoroutine = null; 
    }

    private void AttackMonster(GameObject targetMonster)
    {
        // var newBullet = Instantiate(bulletPrefab, barrel.transform.position, Quaternion.identity);
        var newBullet = BulletPool.instance.GetObjectFromPool(TowerType.Runeblast, barrel.transform.position, barrel.transform.rotation);
        newBullet.transform.rotation = barrel.transform.rotation;
        var bullet = newBullet.GetComponent<BaseBullet>();
        bullet.Damage = this.damage;
        bullet.TargetMonster = targetMonster;
    }
   
}