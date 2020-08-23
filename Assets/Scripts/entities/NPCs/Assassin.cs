using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Character
{
    public Transform kingTransform;
    Character charInTheWay;
    public GameObject corpse;    


    private void Start()
    {
        KingAI king = FindObjectOfType<KingAI>();
        if (king == null)
            return;
        kingTransform = king.transform;
    }

    public override void CharacterUpdate()
    {        
        Mover.MoveCharacterToWayPoint(
            this,
            out charInTheWay,
            true,
            kingTransform.position,
            Physics.DefaultRaycastLayers);
    }

    public override void Kill(Vector3 impactForce)
    {
        GameObject corpseInstance = Instantiate(corpse, transform.position, transform.rotation);
        corpseInstance.GetComponent<Rigidbody>().AddForce(
            impactForce * deathForceMultiplier, 
            ForceMode.Impulse);
        Destroy(gameObject);
        Destroy(this);
    }

    public override bool Interact(Character user)
    {
        currDir = user.currDir;
        switch (user.type)
        {
            case CharacterType.PEASANT:
                //If a peasant is pushed and there is an assasin in the push direction, this will push the assasin
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
            case CharacterType.KING:
                //Code where the king dies (in minecraft)
                user.Kill(currDir.Vector());

                currDir = MovDir.NONE;
                return false;
            case CharacterType.ASSASIN:
                //Assasins push eachother
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                return false;
            case CharacterType.PLAYER:
                Kill(user.currDir.Vector());
                return false;
            default:
                currDir = MovDir.NONE;
                return false;
        }
    }

}
