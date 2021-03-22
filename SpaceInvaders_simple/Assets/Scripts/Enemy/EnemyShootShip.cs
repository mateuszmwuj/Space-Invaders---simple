using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootShip : EnemyBasicShip
{
    [SerializeField]
    private EnemyShoot _enemyShoot;
    public EnemyShoot enemyShoot { get { return _enemyShoot; } }

    // Start is called before the first frame update
    void Start()
    {
        _enemyShoot = gameObject.GetComponent<EnemyShoot>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
