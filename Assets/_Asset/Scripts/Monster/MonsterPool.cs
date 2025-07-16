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
    private EnemySpawnInfo spawnInfo;
    private MapManager mapManager => MapManager.instance;
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

    public GameObject GetObjectFromPool(MonsterType poolName, Vector3 position, Quaternion rotation,EnemySpawnInfo spawnInfo)
    {
        var monsterName = poolName.ToString();
        this.spawnInfo = spawnInfo;
        return GetObjectFromPool(monsterName, position, rotation);
    }

    public override void OnSpawn(GameObject obj)
    {
        mapManager.ActiveMonsters.Add(obj);
        mapManager.MonsterCount++;
        var monsterBase = obj.GetComponent<BaseMonster>();
        monsterBase.Initialize(spawnInfo);
    }

    public override void OnReturn(GameObject obj)
    {
        mapManager.ActiveMonsters.Remove(obj);
        mapManager.MonsterCount--;
    }
}