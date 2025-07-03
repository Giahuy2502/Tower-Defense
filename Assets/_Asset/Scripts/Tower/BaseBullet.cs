using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 1f;
    [SerializeField] protected float bulletLifeTime = 2f;
    [SerializeField] protected float rotateSpeed = 2f;
    [SerializeField] private GameObject targetMonster;
    [SerializeField] private float damage = 10f;

    public GameObject TargetMonster
    {
        get => targetMonster;
        set => targetMonster = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }
    public void Update()
    {
        Move(targetMonster);
    }
/// <summary>
/// if targetMonster == null, just move bullet forward
/// </summary>
/// <param name="targetMonster"></param>
    private void Move(GameObject targetMonster = null)
    {
        if (targetMonster == null)
        {
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
            return;
        }
        var baseMonster = this.targetMonster.GetComponent<BaseMonster>();
        var targetPos = baseMonster.TargetPos;
        transform.LookAt(targetPos);
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            var monster = other.gameObject.GetComponent<BaseMonster>();
            monster.TakeDamage(damage);
            BulletPool.instance.ReturnObjectToPool(gameObject);
        }
    }
    
}
