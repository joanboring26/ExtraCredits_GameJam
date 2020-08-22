using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : Character
{
    Character charInTheWay;
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
                break;
            case CharacterType.KING:
                Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers);
                //Code that makes the peasant pissed off

                //
                currDir = MovDir.NONE;
                return false;
                break;
            case CharacterType.PLAYER:
                Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers);
                currDir = MovDir.NONE;
                return false;
                break;
            case CharacterType.PLANT:
                currDir = MovDir.NONE;
                return true;
                break;
            case CharacterType.OBSTACLE:
                currDir = MovDir.NONE;
                return true;
                break;
            case CharacterType.NONE:
                currDir = MovDir.NONE;
                return true;
                break;
            default:
                currDir = MovDir.NONE;
                return false;
                break;
        }
    }

}
