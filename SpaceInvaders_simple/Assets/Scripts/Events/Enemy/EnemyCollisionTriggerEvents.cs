using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionTriggerEvents : MonoBehaviour
{ 
    public static Action DownWallEnter;    
    
    public static Action PlayerContact;

    public static Action<int> LaserWithPlayerContact;
}
