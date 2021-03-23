using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserShot : LaserShot
{
    void Start()
    {
        direction = 1;

        ignoreTagShip = TagsData.player;
        ignoreTagLaser = TagsData.playerLaserShot;

        targetTagShip = TagsData.enemy;
        targetTagLaser = TagsData.enemyLaserShot;
    }
}
