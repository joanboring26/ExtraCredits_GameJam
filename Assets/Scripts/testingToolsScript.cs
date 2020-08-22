using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingToolsScript : MonoBehaviour
{

    [SerializeField] KeyCode increaseHealthbar;
    [SerializeField] KeyCode decreaseHealthbar;
    [SerializeField] KeyCode speedUpTime;
    [SerializeField] KeyCode showAlert;
    [SerializeField] float timeMultiplier;
    [SerializeField] uiMasterScript ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(increaseHealthbar))
            ui.DamageKing(-0.1f);
        if(Input.GetKeyDown(decreaseHealthbar))
            ui.DamageKing(0.1f);
        if(Input.GetKeyDown(speedUpTime))
            if(Time.timeScale == 1)
                Time.timeScale = timeMultiplier;
            else
                Time.timeScale = 1;
        if(Input.GetKeyDown(showAlert))
            StartCoroutine(ui.ShowAlertText());
    }
}
