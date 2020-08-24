using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : Character
{
    public static int peasantDeaths = 0;

    //Every time a peasant is pushed, be it by another peasant, the player, 
    //or the king, their anger limit goes down, 
    //if it reaches 0, the peasant transforms into an assasin character
    //public int AngerLimit;
    Character charInTheWay = null;
    [Range(0,1)]
    public float chanceToMove = .2f;
    public MovDir directionForNextTurn = MovDir.NONE;
    public AudioSource sndSrc;
    public AudioClip dead;
    [SerializeField] GameObject [] movementArrows = null;

    void Start()
    {
        foreach(GameObject g in movementArrows)
            g.SetActive(false);
    }
    public override void Die(Vector3 impactForce)
    {
        peasantDeaths++;
        foreach(GameObject g in movementArrows)
            g.SetActive(false);
        if (sndSrc != null)
        {
            sndSrc.PlayOneShot(dead);
        }
        base.Die(impactForce);        
    }

    public override void CharacterUpdate()
    {
        // Move in direction set last turn
        if (directionForNextTurn != MovDir.NONE)
        {
            Mover.MoveCharacter(
                this,
                out charInTheWay,
                false,
                directionForNextTurn,
                Physics.DefaultRaycastLayers);
            movementArrows[(int)directionForNextTurn].SetActive(false);
        }
        // Pick direction for next turn
        if (Random.value <= chanceToMove)
        {
            directionForNextTurn = (MovDir)Random.Range(0, 4);
            movementArrows[(int)directionForNextTurn].SetActive(true);
        }
        else
        {
            directionForNextTurn = MovDir.NONE;            
        }
    }

    public override bool Interact(Character user)
    {
        currDir = user.currDir;

        if (user.type == CharacterType.KING)
        {
            currDir = MovDir.NONE;
            return false;
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

}
