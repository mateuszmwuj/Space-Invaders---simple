using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShipSoundsEvent : MonoBehaviour
{
    public static Action<AudioClip> PlayerShootSound;
    public static Action<AudioClip> PlayerExplodeSound;
}
