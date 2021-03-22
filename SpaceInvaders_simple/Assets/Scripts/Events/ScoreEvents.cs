using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEvents : MonoBehaviour
{
    public static Action ScoreReset;
    public static Action<int> ScoreAdd;
    public static Action<int> ScoreSub;
    public static Action<int> ScoreEnemyAmount;
    public static Action<int> ScoreSync;
    public static Action<int> PlayerHitByEnemiesLaser;
}
