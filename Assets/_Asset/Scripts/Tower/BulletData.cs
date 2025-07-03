using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
[CreateAssetMenu(fileName = "BulletData",menuName = "TowerDefense/BulletData")]
public class BulletData : ScriptableObject
{
    [SerializeField] private List<Bullet> bullets = new List<Bullet>();
    public List<Bullet> Bullets => bullets;
}

[System.Serializable]
public class Bullet
{
    public TowerType towerType;
    public GameObject bulletPrefab;
}