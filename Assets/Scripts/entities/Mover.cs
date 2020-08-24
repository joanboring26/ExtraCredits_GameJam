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
        if (direction == MovDir.NONE)
            return false;
        layerMask &= ~LayerMask.GetMask("Corpses");

        //Do raycast
        Vector3 dir = direction.Vector();
        Vector3 sourcePos = 
            sourceCharacter.transform.position + 
            sourceCharacter.rayStartingHeight;
        RaycastHit rayInfo;
        Physics.Linecast(sourcePos , sourcePos + dir, out rayInfo, layerMask);
        //Debug.DrawLine(sourcePos, sourcePos + dir, Color.red);

        //If we hit something, check if it blocks our path
        bool isBlocked = rayInfo.transform != null;
        if (isBlocked)
        {
            //Check if the blocking thing is a character
            hitCharacter = rayInfo.transform.gameObject.GetComponent<Character>();
            if (hitCharacter == null)
                return false;
            
            //If the function cant interact with objects then ignore the hit object and dont move, if it can, do an interact
            if (!pushHitCharacter)
            {
                return false;
            }
            //If the character we interact with tells us that we shouldnt move, 
            //we return false and we dont move
            //if it doesnt, then we move
            if (!hitCharacter.Interact(sourceCharacter))
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
        sourceCharacter.currDir = MovDir.NONE;
        return true;
    }

    public static bool MoveCharacterToWayPoint(
        Character sourceCharacter, 
        out Character hitCharacter,
        bool pushHitCharacter,
        Vector3 waypoint,
        LayerMask layerMask)
    {
        MovDir primaryDir;
        MovDir secondaryDir;
        WaypointDirection(
            sourceCharacter, 
            waypoint, 
            out primaryDir, 
            out secondaryDir);
        sourceCharacter.currDir = primaryDir;
        // try moving in primary direction first
        bool firstMoveWorked =
            MoveCharacter(
            sourceCharacter,
            out hitCharacter,
            true,
            sourceCharacter.currDir,
            Physics.DefaultRaycastLayers);
        bool secondMoveWorked = false;
        // if that failed, then try secondary direction
        if (!firstMoveWorked)
        {
            sourceCharacter.currDir = secondaryDir;
            secondMoveWorked =
                MoveCharacter(
                sourceCharacter,
                out hitCharacter,
                true,
                sourceCharacter.currDir,
                Physics.DefaultRaycastLayers);
        }
        sourceCharacter.currDir = MovDir.NONE;
        return firstMoveWorked || secondMoveWorked;
    }

    static void WaypointDirection(
        Character character, 
        Vector3 waypoint,
        out MovDir primaryDirection,
        out MovDir secondaryDirection)
    {
        Vector3 vector = waypoint - character.transform.position;
        bool forwardLeft = vector.z > vector.x;
        bool forwardRight = vector.z + vector.x > 0;
        bool forward = vector.z > 0;
        bool right = vector.x > 0;        

        if (forwardLeft)
        {
            if (forwardRight)
            {
                primaryDirection = MovDir.FORWARD;
                secondaryDirection = right ? MovDir.RIGHT : MovDir.LEFT;
            }
            else // backLeft
            {
                primaryDirection = MovDir.LEFT;
                secondaryDirection = forward ? MovDir.FORWARD : MovDir.BACK;
            }
        }
        else // backRight
        {
            if (forwardRight)
            {
                primaryDirection = MovDir.RIGHT;
                secondaryDirection = forward ? MovDir.FORWARD : MovDir.BACK;
            }
            else // backLeft
            {
                primaryDirection = MovDir.BACK;
                secondaryDirection = right ? MovDir.RIGHT : MovDir.LEFT;
            }
        }
    }
}
