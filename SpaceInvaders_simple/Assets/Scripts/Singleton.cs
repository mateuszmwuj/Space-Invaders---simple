using UnityEngine;

public class Singleton : MonoBehaviour
{
    protected static Singleton _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            //Rest of your Awake code
        }
        else
        {
            Destroy(this);
        }
    }
}