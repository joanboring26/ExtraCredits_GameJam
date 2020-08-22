using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //This class will only define interaction functions between the cells
    public CharacterType type;

    public virtual bool Interact(Character user)
    {
        return true;
    }

    public virtual void CharacterUpdate()
    {

    }    

}

public enum CharacterType
{
    PEASANT,
    KING,
    PLAYER,
    PLANT,
    OBSTACLE,
    NONE
}

public enum MovDir
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT
}
