using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class King : Character
{
    [SerializeField]
    GameObject[] waypoint = null;

    public toDoListScript listOfTasks;
    Dictionary<int, Transform> kingObjectives;
    public int currObjective;

    public int ActionDelay;
    int currDelay;

    //This will be the king's healthbar object
    //public GameObject KingHealthBar;
    [SerializeField] uiMasterScript UI;
    public AudioSource sndSrc;
    public AudioClip[] kingDamaged;
    public AudioClip kingDead;
    //public int health;

    Character charInTheWay;

    void Start()
    {
        kingObjectives = new Dictionary<int, Transform>();
        currDelay = ActionDelay;
        for (int i = 0; i < waypoint.Length; i++)
        {
            kingObjectives.Add((int)waypoint[i].GetComponent<ObjBase>().taskType, waypoint[i].transform);
        }
    }

    public void DamageEffects()
    {
        sndSrc.PlayOneShot(kingDamaged[Random.Range(0, kingDamaged.Length)]);
    }

    //public override void Kill(Vector3 impactForce)
    public void Kill(Vector3 impactForce)
    {
        UI.DamageKing(0.25f);
        DamageEffects();

        if (UI.GetHealthValue() <= 0)
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
            Kill(user.currDir.Vector());
            if (UI.GetHealthValue() > 0)
            {
                Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers);
                return true;
            }
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
