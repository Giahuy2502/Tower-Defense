using _Asset.Scripts.MyAsset;
using MarchingBytes;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPool : EasyObjectPool
{
    [Header("Data ")]
    [SerializeField] private MonsterDatas data;
    [SerializeField] private int monsterPoolSize;
    [SerializeField] private bool monsterFixedSize;
    public static new MonsterPool instance;

    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        GetData();
    }

    public override void GetData()
    {
        var poolInfo = base.poolInfo;
        foreach (var monster in data.Datas)
        {
            PoolInfo newPoolInfo = new PoolInfo();
            newPoolInfo.poolName = monster.monsterName;
            newPoolInfo.poolSize = monsterPoolSize;
            newPoolInfo.fixedSize = monsterFixedSize;
            newPoolInfo.prefab = monster.monsterPrefab;
            poolInfo.Add(newPoolInfo);
        }
    }

    public GameObject GetObjectFromPool(MonsterType poolName, Vector3 position, Quaternion rotation)
    {
        var monsterName = poolName.ToString();
        return GetObjectFromPool(monsterName, position, rotation);
    }

    public override void OnSpawn(GameObject obj)
    {
        // Ví dụ: Khi enemy spawn, bật animation xuất hiện
        // var enemy = obj.GetComponent<Enemy>();
        // if (enemy != null)
        // {
        //     enemy.PlaySpawnEffect();
        // }
    }

    public override void OnReturn(GameObject obj)
    {
       
        // Ví dụ: Reset máu khi enemy được trả về
        // var enemy = obj.GetComponent<Enemy>();
        // if (enemy != null)
        // {
        //     enemy.ResetHealth();
        // }
    }
}