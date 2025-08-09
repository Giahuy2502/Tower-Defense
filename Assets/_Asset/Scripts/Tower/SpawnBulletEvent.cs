using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletEvent : MonoBehaviour
{
    private TowerAttack _towerAttack;

    private void Awake()
    {
        _towerAttack = GetComponentInParent<TowerAttack>();
    }

    private void SpawnBullet()
    {
        _towerAttack.SpawnBullet();
    }
}
