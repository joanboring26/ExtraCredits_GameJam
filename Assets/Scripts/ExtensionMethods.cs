using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class ExtensionMethods 
{    
    public static Vector3 Vector(this MovDir direction)
    {
        return direction == MovDir.BACK ? Vector3.back :
            direction == MovDir.FORWARD ? Vector3.forward :
            direction == MovDir.LEFT ? Vector3.left :
            Vector3.right;
    }
}
