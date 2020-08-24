using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int hp;
    Character charInTheWay;   

    public override bool Interact(Character user)
    {
        currDir = user.currDir;
        switch (user.type)
        {
            
            case CharacterType.KING:
                //The king can push around the player
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
            case CharacterType.ASSASIN:
                user.Kill(-user.currDir.Vector());
                return true;
            case CharacterType.PLAYER:
                //This shouldn't really trigger, ever, but it's here just in case
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true; 
                }
                currDir = MovDir.NONE;
                return false;
            default:
                currDir = MovDir.NONE;
                return false;
        }

        // This character can be pushed and will push other 
        // characters that it is allowed to push
        if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
        {
            return true;
        }
        currDir = MovDir.NONE;
        return false;
    }
}