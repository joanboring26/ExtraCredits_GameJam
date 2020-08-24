using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class King : Character
{
    [SerializeField] int numberOfHitsToKillKing = 4;
    ObjectiveManager objectiveManager;
    public GameObject questionMark;
    [SerializeField] GameObject[] movementArrows = null;
    public int ActionDelay;
    public int InputDelay;
    int currDelay;
    int currInputDelay;

    //This will be the king's healthbar object
    //public GameObject KingHealthBar;
    public AudioSource sndSrc;
    public AudioClip[] kingDamaged;
    public AudioClip kingDead;
    public AudioClip question;
    //public int health;

    void Start()
    {
        questionMark.SetActive(false);
        currDelay = ActionDelay;
        currInputDelay = InputDelay;
        objectiveManager = GetComponent<ObjectiveManager>();

        for (int i = 0; i < movementArrows.Length; i++)
        {
            movementArrows[i].SetActive(false);
        }
//        movementArrows[(int)currDir].SetActive(true);
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

    public void Update()
    {
        if (currInputDelay <= 0)
        {
            for (int i = 0; i < movementArrows.Length; i++)
            {
                movementArrows[i].SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                currDir = MovDir.FORWARD;
                currInputDelay = InputDelay;
                questionMark.SetActive(false);
                movementArrows[(int)currDir].SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currDir = MovDir.BACK;
                currInputDelay = InputDelay;
                questionMark.SetActive(false);
                movementArrows[(int)currDir].SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                currDir = MovDir.LEFT;
                currInputDelay = InputDelay;
                questionMark.SetActive(false);
                movementArrows[(int)currDir].SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                currDir = MovDir.RIGHT;
                currInputDelay = InputDelay;
                questionMark.SetActive(false);
                movementArrows[(int)currDir].SetActive(true);
            }
            else
            {
                return;
            }
        }
    }

    public override void CharacterUpdate()
    {
        currInputDelay -= 1;
        if(currInputDelay <= 0)
        {
            currInputDelay = 0;
            questionMark.SetActive(true);
        }


        if (currDelay <= 0)
        {
            currDelay = ActionDelay;            
            //Vector3 waypointPos = objectiveManager.Waypoint;
            Character hitCharacter = null;
            MovDir backup = currDir;
            Mover.MoveCharacter(this, out charInTheWay, true, currDir, Physics.DefaultRaycastLayers);
            currDir = backup;
            /*
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
            */
            objectiveManager.AttemptToCompleteObjective();
        }
        else
        {
            currDelay -= 1;
        }
    }    
}
