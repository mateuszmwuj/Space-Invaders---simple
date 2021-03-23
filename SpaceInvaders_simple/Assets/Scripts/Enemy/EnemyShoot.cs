using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Shoot
{
    public void Init(int amountOfCashedLaserShots, int indexOfShooter = -1)
    {
        this.amountOfCashedLaserShots = amountOfCashedLaserShots;

        this.indexOfShooter = indexOfShooter;

        InstantiateLaserShots(this.amountOfCashedLaserShots, indexOfShooter);
    }
}
