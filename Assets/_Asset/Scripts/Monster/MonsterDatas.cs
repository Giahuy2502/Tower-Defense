using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private string monsterName;
    [SerializeField] private float monsterHealth;
    [SerializeField] private float monsterDamage;
    [SerializeField] private float monsterSpeed;
    [SerializeField] private GameObject monsterPrefab;
}