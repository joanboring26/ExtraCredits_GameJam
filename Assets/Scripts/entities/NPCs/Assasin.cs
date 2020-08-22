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
        Mover.MoveCharacterToWayPoint(
            this,
            out charInTheWay,
            true,
            kingTransform.position,
            Physics.DefaultRaycastLayers);
    }

}
