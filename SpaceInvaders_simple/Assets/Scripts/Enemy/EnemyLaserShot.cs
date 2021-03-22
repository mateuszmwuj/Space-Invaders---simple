using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserShot : LaserShot
{
    // Start is called before the first frame update
    void Awake()
    {
        direction = -1;

        ignoreTagShip = TagsData.enemy;
        ignoreTagLaser = TagsData.enemyLaserShot;
        
        targetTagShip = TagsData.player;
        targetTagLaser = TagsData.playerLaserShot;
    }

    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag(targetTagShip))
        {
            EnableLaser(false);

            DisableGameObject();
        }
    }
}
