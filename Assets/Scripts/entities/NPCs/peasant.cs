using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : Character
{
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
    [SerializeField] GameObject forwardArrow = null;
    [SerializeField] GameObject backwardArrow = null;
    [SerializeField] GameObject leftArrow = null;
    [SerializeField] GameObject rightArrow = null;

    void Start()
    {
        forwardArrow.SetActive(false);
        backwardArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }
    public override void Kill(Vector3 impactForce)
    {
        sndSrc.PlayOneShot(dead);
        TimeKeeper.Deregister(this);
        Rigidbody rb = modelTransform.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            int corpsesLayer = LayerMask.NameToLayer("Corpses");
            gameObject.layer = corpsesLayer;
            modelTransform.gameObject.layer = corpsesLayer;
            rb.isKinematic = false;
            rb.AddForce(impactForce * deathForceMultiplier, ForceMode.Impulse);
        }
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
        }
        // Pick direction for next turn
        if (Random.value <= chanceToMove)
        {
            directionForNextTurn = (MovDir)Random.Range(0, 4);
        }
        else
        {
            directionForNextTurn = MovDir.NONE;            
        }
    }

    public override bool Interact(Character user)
    {
        currDir = user.currDir;
        switch (user.type)
        {
            case CharacterType.PEASANT:
                //If a peasant is pushed and there is another peasant in the push 
                //direction, this will trigger on the pushed peasant
                //If the peasant moves in a direction that is free, tell the other 
                //peasant that pushed us that he can move to our previous direction
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
            case CharacterType.KING:
                // Peasants are obstacles for the king, so he can't push them
                //if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                //{
                //    return true;
                //}

                currDir = MovDir.NONE;
                return false;
            case CharacterType.ASSASIN:
                Kill(user.currDir.Vector());
                return false;
            case CharacterType.PLAYER:
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                    return true;

                currDir = MovDir.NONE;
                return false;
            default:
                currDir = MovDir.NONE;
                return false;
        }
    }

}
