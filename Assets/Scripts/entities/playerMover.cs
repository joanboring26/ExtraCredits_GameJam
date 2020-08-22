using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMover : MonoBehaviour
{
    public LayerMask movementMask;
    public CellBase playerCell;

    //Function that checks if there is an entity in the given movement direction
    public bool CheckDir(MovDir direction)
    {
        //Set the raycast direction
        Vector3 dir = new Vector3(0, 0, 0);
        switch (direction)
        {
            case MovDir.FORWARD:
                dir.z = 1;
                break;
            case MovDir.BACK:
                dir.z = -1;
                break;
            case MovDir.LEFT:
                dir.x = -1;
                break;
            case MovDir.RIGHT:
                dir.x = 1;
                break;
            default:
                break;
        }
        //Do raycast
        RaycastHit rayInfo;
        Physics.Linecast(transform.position, transform.position + dir, out rayInfo, movementMask);
        Debug.DrawLine(transform.position, transform.position + dir, Color.red);

        //Check if we even hit anything
        if (rayInfo.transform != null)
        {
            //Check if the thing we hit is another entity
            CellBase otherCell = rayInfo.transform.gameObject.GetComponent<CellBase>();
            if (otherCell != null)
            {
                if (otherCell.CellInteract(playerCell))
                {
                    playerCell.currDir = dir;
                    return true;
                }
                return false;
            }
        }
        playerCell.currDir = dir;
        return true;
    }


    public void MoveInDir(MovDir direction)
    {
        if(CheckDir(direction))
        {
            playerCell.MoveToDir(playerCell.currDir);
        }

    }

}
