using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assasin : Character
{
    public Transform kingTransform;
    Character charInTheWay;

    private void Start()
    {
        //kingTransform = FindObjectOfType<kingAIScript>();
    }

    public override void CharacterUpdate()
    {
        float distX = transform.position.x - kingTransform.position.x;
        float distZ = transform.position.z - kingTransform.position.z;
        if(Mathf.Abs(distX) > Mathf.Abs(distZ))
        {
            if(distX > 0)
            {
                currDir = MovDir.LEFT;
            }
            else
            {
                currDir = MovDir.RIGHT;
            }
        }
        else
        {
            if (distZ > 0)
            {
                currDir = MovDir.BACK;
            }
            else
            {
                currDir = MovDir.FORWARD;
            }
        }
        Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers);
        currDir = MovDir.NONE;
    }

}
