using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Character
{
    Transform kingTransform;
    //public GameObject corpse;    

    public AudioSource sndSrc;
    public AudioClip dead;
    public float aggroRadius;

    public override void Die(Vector3 impactForce)
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
        King king = FindObjectOfType<King>();
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
        if(user.type == CharacterType.PLAYER)
        {
            Die(user.currDir.Vector());
            return true;
        }
        else if (user.type == CharacterType.KING)
        {
            // If the king tries to walk into an assassin the king dies
            user.Die(-user.currDir.Vector());
            return false;
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
