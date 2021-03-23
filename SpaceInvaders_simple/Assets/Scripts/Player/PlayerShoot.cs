using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : Shoot
{
    protected new void Update()
    {
        base.Update();

        ShootLaser(SpaceShipsTypes.Player);
    }
}
