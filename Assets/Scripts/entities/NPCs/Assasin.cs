using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Character
{
    public Transform kingTransform;

    private void Start()
    {
        //kingTransform = FindObjectOfType<kingAIScript>();
    }

    public override void CharacterUpdate()
    {
        float distX = transform.position.x - kingTransform.position.x;
        float distZ = transform.position.z - kingTransform.position.z;
        Vector3 dir = new Vector3(0, 0, 0);
        if(distX > distZ)
        {
            if(distX > 0)
            {
                dir.x = 1;
            }
            else
            {
                dir.x = 1;
            }
        }
    }

}
