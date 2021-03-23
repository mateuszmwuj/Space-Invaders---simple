using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingShip : EnemyBasicShip
{
    [SerializeField]
    private EnemyShoot _enemyShoot;
    public EnemyShoot enemyShoot =>_enemyShoot;
}
