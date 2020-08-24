using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbarScript : MonoBehaviour
{
    [SerializeField] GameObject healthbar = null;
    float healthValue;
    void Awake()
    {
        healthValue = healthbar.transform.localScale.x;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(float amount)
    {
        healthValue += amount;
        healthbar.transform.localScale = new Vector3(healthValue, 1, 1);
        if(healthValue < 0)
            healthValue = 0;
        else if(healthValue > 1)
            healthValue = 1;
        healthbar.transform.localScale = new Vector3(healthValue, 1, 1);
    }

    public float GetHealthValue()
    {
        return healthValue;
    }
}
