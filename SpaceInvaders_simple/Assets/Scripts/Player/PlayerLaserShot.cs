using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserShot : LaserShot
{
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;

        ignoreTagShip = TagsData.player;
        ignoreTagLaser = TagsData.playerLaserShot;


        targetTagShip = TagsData.enemy;
        targetTagLaser = TagsData.enemyLaserShot;
    }
}
