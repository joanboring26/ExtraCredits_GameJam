using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : Character
{
    //Every time a peasant is pushed, be it by another peasant, the player, or the king, their anger limit goes down, 
    //if it reaches 0, the peasant transforms into an assasin character
    public int AngerLimit;
    Character charInTheWay;
    public GameObject corpse;

    public override void Kill(Vector3 impactForce)
    {
        GameObject corpseInstance = Instantiate(corpse, transform.position, transform.rotation);
        corpseInstance.GetComponent<Rigidbody>().AddForce(impactForce, ForceMode.Impulse);
        Destroy(gameObject);
    }

    public override bool Interact(Character user)
    {
        currDir = user.currDir;
        switch (user.type)
        {
            case CharacterType.PEASANT:
                //If a peasant is pushed and there is another peasant in the push direction, this will trigger on the pushed peasant
                //If the peasant moves in a direction that is free, tell the other peasant that pushed us that he can move to our previous direction
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
            case CharacterType.KING:
                Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers);
                //Code that makes the peasant pissed off

                //
                currDir = MovDir.NONE;
                return false;
                break;
            case CharacterType.ASSASIN:
                Kill(user.currDir.Vector());
                return false;
                break;
            case CharacterType.PLAYER:
                Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers);
                currDir = MovDir.NONE;
                return false;
                break;
            default:
                currDir = MovDir.NONE;
                return false;
        }
    }

}
