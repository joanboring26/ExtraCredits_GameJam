using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxWaypointScript : MonoBehaviour
{
    [SerializeField] uiMasterScript userInterface;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        userInterface.ShowText("There's the guy who owes me money! Get him!");
    }
}
