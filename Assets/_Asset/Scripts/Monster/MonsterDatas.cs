using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
[CreateAssetMenu(fileName = "MonsterData", menuName = "TowerDefense/MonsterData")]
public class MonsterDatas : ScriptableObject
{
    [SerializeField] private List<MonsterData> monsterDatas = new ();

    public List<MonsterData> Datas => monsterDatas;
}

[System.Serializable]
public class MonsterData
{
    [SerializeField] public MonsterType monsterType;
    [SerializeField] public float monsterHealth;
    [SerializeField] public float monsterDamage;
    [SerializeField] public float monsterSpeed;
    [SerializeField] public GameObject monsterPrefab;
    public string monsterName => monsterType.ToString();
}