using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //This class will only define interaction functions between the cells
    public CharacterType type = CharacterType.NONE;
    public MovDir currDir = MovDir.NONE;
    [HideInInspector] public Vector3 prevModelPos;
    public float moveSpeed = 10;
    protected Transform modelTransform = null;
    public Coroutine moveModelCoroutine = null;
    public float deathForceMultiplier = 8;
    [HideInInspector] protected uiMasterScript ui;
    public readonly Vector3 rayStartingHeight = new Vector3(0, .1f, 0);
    protected Character charInTheWay = null;

    private void Awake()
    {
        ui = FindObjectOfType<uiMasterScript>();
        TimeKeeper.Register(this);
        if (transform.childCount > 0)
            modelTransform = transform.GetChild(0);
    }

    private void OnDestroy()
    {
        TimeKeeper.Deregister(this);
    }    

    public virtual bool Interact(Character user)
    {
        return false;
    }

    protected bool DieByPlayerHoldingShiftKey(Character user)
    {
        if (user.type == CharacterType.PLAYER &&
            PlayerInput.isKillingMove)
        {
            Die(user.currDir.Vector());
            return true;
        }
        return false;
    }

    public virtual void Die(Vector3 impactForce)
    {
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

    public virtual void CharacterUpdate()
    {

    }

    public void MoveModelToPos( Vector3 finalPosition)
    {
        modelTransform.position = prevModelPos;
        if (moveModelCoroutine != null)
        {
            StopCoroutine(moveModelCoroutine);
        }
        moveModelCoroutine = StartCoroutine(MoveModel( finalPosition));
    }

    public virtual IEnumerator MoveModel( Vector3 finalPosition)
    {
        while((finalPosition - modelTransform.position).magnitude > 0.2f)
        {
            modelTransform.position += (finalPosition - modelTransform.position).normalized * moveSpeed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        modelTransform.position = finalPosition;
        moveModelCoroutine = null;
    }
}

public enum CharacterType
{
    PEASANT,
    KING,
    PLAYER,
    PLANT,
    ASSASIN,
    OBSTACLE,
    TAXCOLLECTOR,
    NONE
}

public enum MovDir
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT,
    NONE
}
