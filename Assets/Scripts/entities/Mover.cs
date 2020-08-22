using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Mover
{    
    public static bool MoveCharacter(
        Character sourceCharacter, 
        out Character hitCharacter, 
        bool pushHitCharacter,
        MovDir direction,
        LayerMask layerMask)
    {
        hitCharacter = null;

        //Do raycast
        Vector3 dir = direction.Vector();
        Vector3 sourcePos = sourceCharacter.transform.position;
        RaycastHit rayInfo;
        Physics.Linecast(sourcePos, sourcePos + dir, out rayInfo, layerMask);
        Debug.DrawLine(sourcePos, sourcePos + dir, Color.red);

        //If we hit something, check if it blocks our path
        bool isBlocked = rayInfo.transform != null;
        if (isBlocked)
        {
            //Check if the blocking thing is a character
            hitCharacter = rayInfo.transform.gameObject.GetComponent<Character>();
            if (hitCharacter != null)
            {
                //If the function cant interact with objects then ignore the hit object and dont move, if it can, do an interact
                if (!pushHitCharacter)
                {
                    return false;
                }
                else
                {
                    //If the character we interact with tells us that we shouldnt move, we return false and we dont move
                    //if it doesnt, then we move
                    if (!hitCharacter.Interact(sourceCharacter))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        //If the way is clear, then move sourceCharacter and return true
        //The movements should ONLY be done in 1 unit increment 
        //ie: I move right so dir = 1,0,0    
        sourceCharacter.prevModelPos = sourceCharacter.transform.position;
        sourceCharacter.transform.position += dir;
        sourceCharacter.MoveModelToPos(sourceCharacter.transform.position);
        return true;
    }

    public static MovDir WaypointDirection(Character character, Vector3 waypoint)
    {
        Vector3 vector = waypoint - character.transform.position;
        bool forwardLeft = vector.z > vector.x;
        bool forwardRight = vector.z + vector.x > 0;
        if (forwardLeft)
        {
            if (forwardRight)
            {
                return MovDir.FORWARD;
            }
            else 
            {
                return MovDir.LEFT;
            }
        }
        else
        {
            if (forwardRight)
            {
                return MovDir.RIGHT;
            }
            else
            {
                return MovDir.BACK;
            }
        }
    }
}
