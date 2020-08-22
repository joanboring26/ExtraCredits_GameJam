using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    static TimeKeeper instance;
    public TimeKeeper Instance { get { return instance; } }



    void Awake()
    {
        DoSingletonPattern();
    }    

    void DoSingletonPattern()
    {
        if (instance != null && this != instance)
            Destroy(this.gameObject);
        else
            instance = this;
    }
}
