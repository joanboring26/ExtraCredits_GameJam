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
        if (user.type == CharacterType.ASSASIN) 
        { 
            user.Die(-user.currDir.Vector());
            return false;
        }
        if (user.type == CharacterType.KING)
        {
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