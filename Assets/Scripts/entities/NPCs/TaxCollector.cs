using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxCollector : Character
{
    public AudioSource sndSrc;
    public AudioClip dead;
    King king;
    public float runRadius;

    private void Start()
    {
        king = FindObjectOfType<King>();
    }

    public override bool Interact(Character user)
    {
        currDir = user.currDir;
        if (DieByPlayerHoldingShiftKey(user))
        {
            return true;
        }
        if (user.type == CharacterType.ASSASIN)
        {
            Die(user.currDir.Vector());
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

    public override void Die(Vector3 impactForce)
    {
        if (sndSrc != null)
        {
            sndSrc.PlayOneShot(dead);
        }
        base.Die(impactForce);
    }

    public override void CharacterUpdate()
    {
        if ((transform.position - king.transform.position).magnitude < runRadius)
        {
            // The tax collector should raycast in all 4 directions and go in 
            // whichever direction has the most space and is away from the king
            float[] distances = new float[4];
            int indexOfGreatest = 0;
            for (int i = 0; i < 4; i++)
            {
                // if the ray direction is close to the king's direction 
                //then continue to the next direction
                Vector3 kingPos = king.gameObject.transform.position;
                MovDir rayDir = (MovDir)i;
                MovDir kingDir;
                MovDir dontUseThis;
                Mover.WaypointDirection(
                    this,
                    kingPos,
                    out kingDir,
                    out dontUseThis);
                if (rayDir == kingDir)
                {
                    distances[i] = 0;
                    continue;
                }
                RaycastHit rayInfo;
                LayerMask defaultLayer = LayerMask.NameToLayer("Default");
                Physics.Raycast(
                    transform.position + rayStartingHeight,
                    rayDir.Vector(),
                    out rayInfo,
                    200,
                    1);
                distances[i] = rayInfo.distance;
                if (rayInfo.distance > distances[indexOfGreatest] ||
                    rayInfo.distance == 0)
                {
                    indexOfGreatest = i;
                }
            }
            Mover.MoveCharacter(
                this,
                out charInTheWay,
                true,
                (MovDir)indexOfGreatest,
                Physics.DefaultRaycastLayers);


        }
    }

}
