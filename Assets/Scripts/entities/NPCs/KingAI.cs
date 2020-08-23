using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAI : Character
{
    [SerializeField] GameObject[] waypoint = null;

    public toDoListScript listOfTasks;
    Dictionary<int, Transform> kingObjectives;
    int currObjective;

    public int ActionDelay;
    int currDelay;

    //This will be the king's healthbar object
    public GameObject KingHealthBar;
    public AudioSource sndSrc;
    public AudioClip[] kingDamaged;
    public AudioClip kingDead;
    public int health;

    Character charInTheWay;

    void Start()
    {
        kingObjectives = new Dictionary<int, Transform>();
        currDelay = ActionDelay;
        for(int i = 0; i < waypoint.Length; i++)
        {
            kingObjectives.Add((int)waypoint[i].GetComponent<ObjBase>().taskType, waypoint[i].transform);
        }
    }

    public void DamageEffects()
    {
        sndSrc.PlayOneShot(kingDamaged[Random.Range(0, kingDamaged.Length)]);
    }

    public override void Kill(Vector3 impactForce)
    {
        health -= 1;
        DamageEffects();

        if (health <= 0)
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
        switch (user.type)
        {
            case CharacterType.PEASANT:
                //If a peasant is pushed and theplayer is in the push direction, this will push the player
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
            case CharacterType.KING:
                //This should never trigger, here just in case
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
            case CharacterType.ASSASIN:
                Kill(-user.currDir.Vector());
                return true;
            case CharacterType.PLAYER:
                if (Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers))
                {
                    return true;
                }
                currDir = MovDir.NONE;
                return false;
            default:
                currDir = MovDir.NONE;
                return false;
        }
    }

    public override void CharacterUpdate()
    {
        if (currDelay <= 0)
        {
            currDelay = ActionDelay;
            if (waypoint == null)
                return;
            Vector3 waypointPos = waypoint[currObjective].transform.position;
            Character hitCharacter = null;
            Mover.MoveCharacterToWayPoint(
                this,
                out hitCharacter,
                true,
                waypointPos,
                Physics.DefaultRaycastLayers);
        }
        else
        {
            currDelay -= 1;
        }
    }
}
