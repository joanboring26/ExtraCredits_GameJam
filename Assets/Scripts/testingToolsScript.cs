using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingToolsScript : MonoBehaviour
{

    [SerializeField] KeyCode increaseHealthbar;
    [SerializeField] KeyCode decreaseHealthbar;
    [SerializeField] KeyCode speedUpTime;
    [SerializeField] float timeMultiplier;
    [SerializeField] healthbarScript healthbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(increaseHealthbar))
            healthbar.ChangeHealth(0.1f);
        if(Input.GetKeyDown(decreaseHealthbar))
            healthbar.ChangeHealth(-0.1f);
        if(Input.GetKeyDown(speedUpTime))
            if(Time.timeScale == 1)
                Time.timeScale = timeMultiplier;
            else
                Time.timeScale = 1;
    }
}
