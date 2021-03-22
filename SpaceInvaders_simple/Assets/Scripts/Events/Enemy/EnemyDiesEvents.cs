using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiesEvents : MonoBehaviour
{
    public static Action EnemyDies;
    public static Action<EnemyBasicShip> EnemyDiesWithInfo;
}
