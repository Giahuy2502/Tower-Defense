using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using MarchingBytes;
using UnityEngine;

public class BulletPool : EasyObjectPool
{
    [Header("Bullet Pool Settings")] 
    [SerializeField] private BulletData data;
    [SerializeField] private int bulletPoolSize;
    [SerializeField] private bool bulletFixedSize;
    public static new BulletPool instance;

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
        foreach (var bullet in data.Bullets)
        {
            PoolInfo newPoolInfo = new PoolInfo();
            newPoolInfo.poolName = bullet.towerType.ToString();
            newPoolInfo.poolSize = bulletPoolSize;
            newPoolInfo.fixedSize = bulletFixedSize;
            newPoolInfo.prefab = bullet.bulletPrefab;
            poolInfo.Add(newPoolInfo);
        }
    }

    public GameObject GetObjectFromPool(TowerType poolName, Vector3 position, Quaternion rotation)
    {
        var bulletName = poolName.ToString();
        return GetObjectFromPool(bulletName, position, rotation);
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
