using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class King : Character
{
    [SerializeField] int numberOfHitsToKillKing = 4;
    ObjectiveManager objectiveManager;
    public int ActionDelay;
    int currDelay;

    //This will be the king's healthbar object
    //public GameObject KingHealthBar;
    public AudioSource sndSrc;
    public AudioClip[] kingDamaged;
    public AudioClip kingDead;
    //public int health;

    void Start()
    {
        currDelay = ActionDelay;
        objectiveManager = GetComponent<ObjectiveManager>();
    }

    public void DamageEffects()
    {
        sndSrc.PlayOneShot(kingDamaged[Random.Range(0, kingDamaged.Length)]);
    }

    public override void Die(Vector3 impactForce)
    {
        ui.DamageKing(1f / numberOfHitsToKillKing);
        DamageEffects();

        if (ui.GetHealthValue() <= 0)
        {
            TimeKeeper.Deregister(this);
            Rigidbody rb = modelTransform.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                sndSrc.Stop();
                sndSrc.PlayOneShot(kingDead);
                int corpsesLayer = LayerMask.NameToLayer("Corpses");
                gameObject.layer = corpsesLayer;
                modelTransform.gameObject.layer = corpsesLayer;
                rb.isKinematic = false;
                rb.AddForce(impactForce * deathForceMultiplier, ForceMode.Impulse);
            }
        }
    }

    public override bool Interact(Character user)
    {
        currDir = user.currDir;        
        if (user.type == CharacterType.ASSASIN)
        {
            Die(user.currDir.Vector());            
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

    public override void CharacterUpdate()
    {
        if (currDelay <= 0)
        {
            currDelay = ActionDelay;            
            Vector3 waypointPos = objectiveManager.Waypoint;
            Character hitCharacter = null;
            if (!Mover.MoveCharacterToWayPoint(
                this,
                out hitCharacter,
                true,
                waypointPos,
                Physics.DefaultRaycastLayers))
            {
                Mover.MoveCharacterToWayPoint(
                this,
                out hitCharacter,
                true,
                -waypointPos,
                Physics.DefaultRaycastLayers);
            }
            objectiveManager.AttemptToCompleteObjective();
        }
        else
        {
            currDelay -= 1;
        }
    }    
}
