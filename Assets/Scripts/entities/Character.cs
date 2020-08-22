using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //This class will only define interaction functions between the cells
    public CharacterType type;
    public MovDir currDir;
    public Vector3 prevModelPos;
    public float moveSpeed;
    public Transform modelTransform;
    public Coroutine moveModelCoroutine;

    private void Awake()
    {
        TimeKeeper.Register(this);
    }

    public virtual bool Interact(Character user)
    {
        return false;
    }

    public virtual void Kill(Vector3 impactForce)
    {

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
