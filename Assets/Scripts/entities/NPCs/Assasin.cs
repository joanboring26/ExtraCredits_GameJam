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
        MovDir primaryDirection;
        MovDir secondaryDirection;
        Mover.WaypointDirection(
            this,
            kingTransform.position,
            out primaryDirection,
            out secondaryDirection);
        currDir = primaryDirection;
        if (!Mover.MoveCharacter(
            this,
            out charInTheWay,
            true, 
            currDir,
            Physics.DefaultRaycastLayers))
        {
            currDir = secondaryDirection;
            Mover.MoveCharacter(
                this,
                out charInTheWay,
                true,
                currDir,
                Physics.DefaultRaycastLayers);                
        }
        currDir = MovDir.NONE;
    }

}
