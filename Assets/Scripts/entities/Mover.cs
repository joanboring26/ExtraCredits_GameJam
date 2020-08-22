using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Mover
{    
    public static bool MoveCharacter(
        Character sourceCharacter, 
        out Character hitCharacter, 
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

        //If we hit something, then do no movement and return false
        bool isBlocked = rayInfo.transform != null;
        if (isBlocked)
        {
            hitCharacter = rayInfo.transform.gameObject.GetComponent<Character>();
            return false;
        }

        //If the way is clear, then move sourceCharacter and return true
        //The movements should ONLY be done in 1 unit increment 
        //ie: I move right so dir = 1,0,0    
        sourceCharacter.transform.position += dir;
        return true;
    }    
}
