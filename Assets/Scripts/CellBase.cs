using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    //This class will only define interaction functions between the cells
    public CellTypes type;
    public Vector3 currDir;

    public virtual bool CellInteract(CellBase user)
    {
        return true;
    }

    public virtual void CellUpdate()
    {

    }    

}

public enum CellTypes
{
    PEASANT,
    KING,
    PLAYER,
    PLANT,
    OBSTACLE,
    NONE
}

public enum MovDir
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT
}
