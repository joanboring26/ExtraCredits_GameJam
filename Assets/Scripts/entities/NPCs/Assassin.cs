using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Character
{
    public Transform kingTransform;
    Character charInTheWay;
    //public GameObject corpse;    

    public AudioSource sndSrc;
    public AudioClip dead;
    public float aggroRadius;

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

    private void Start()
    {
        KingAI king = FindObjectOfType<KingAI>();
        if (king == null)
            return;
        kingTransform = king.transform;
    }

    public override void CharacterUpdate()
    {
        if ((transform.position - kingTransform.position).magnitude < aggroRadius)
        {
            Mover.MoveCharacterToWayPoint(
                this,
                out charInTheWay,
                true,
                kingTransform.position,
                Physics.DefaultRaycastLayers);
        }
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
