using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxCollector : Character
{
    Character charInTheWay = null;
    public AudioSource sndSrc;
    public AudioClip dead;

    public override bool Interact(Character user)
    {
        currDir = user.currDir;
        if (user.type == CharacterType.ASSASIN)
        {
            Kill(user.currDir.Vector());
            return true;
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

    public override void Kill(Vector3 impactForce)
    {
        if (sndSrc != null)
        {
            sndSrc.PlayOneShot(dead);
        }
        base.Kill(impactForce);
    }

    public override void CharacterUpdate()
    {

    }

}
