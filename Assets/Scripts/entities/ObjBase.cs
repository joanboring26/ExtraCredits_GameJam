using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBase:Character
{
    public Transform wayPoint;
    public toDoListScript.ToDoTasks taskType;
    public King king;

    public virtual void completed()
    {

    }

    //public override bool Interact(Character user)
    //{
    //    currDir = user.currDir;
    //    if (user.type == CharacterType.KING)
    //    {
    //        completed();
    //        king.listOfTasks.CrossOut(taskType);
    //        currDir = MovDir.NONE;
    //        Destroy(this);
    //        return false;
    //    }
    //    else
    //    {
    //        currDir = MovDir.NONE;
    //        return false;
    //    }
    //}
}
