using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int hp;
    Character charInTheWay;

    private void FixedUpdate()
    {
        // This allows live characters have a rigidbody and move corpses around
        if (modelTransform == null)
            return;
        Rigidbody rb = modelTransform.gameObject.GetComponent<Rigidbody>();
        if (rb == null)
            return;
        rb.velocity = Vector3.zero;
    }

    public override bool Interact(Character user)
    {
        currDir = user.currDir;
        switch (user.type)
        {
            case CharacterType.PEASANT:
                //If a peasant is pushed and theplayer is in the push direction, this will push the player
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
                break;
            case CharacterType.KING:
                //The king can push around the player
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
                break;
            case CharacterType.ASSASIN:
                user.Kill(-user.currDir.Vector());
                return false;
                break;
            case CharacterType.PLAYER:
                //This shouldn't really trigger, ever, but it's here just in case
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
                break;
            default:
                currDir = MovDir.NONE;
                return false;
                break;
        }
    }
}