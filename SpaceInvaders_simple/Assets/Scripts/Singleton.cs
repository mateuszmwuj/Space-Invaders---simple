using System;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    // Start is called before the first frame update
    protected static Singleton instance = null;

    // Game Instance Singleton
    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}