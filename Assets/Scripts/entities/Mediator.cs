using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mediator : MonoBehaviour
{
    static Mediator instance;
    public Mediator Instance { get { return instance; } }



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
