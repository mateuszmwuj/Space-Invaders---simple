using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Shoot
{
    public void Init(int amountOfCachedLaserShots, int indexOfShooter = -1)
    {
        this.amountOfCachedLaserShots = amountOfCachedLaserShots;

        this.indexOfShooter = indexOfShooter;

        InstantiateLaserShots(this.amountOfCachedLaserShots, indexOfShooter);
    }
}
