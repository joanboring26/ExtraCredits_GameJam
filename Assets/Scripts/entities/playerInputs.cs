using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public PlayerMover mover;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            mover.MoveInDir(MovDir.FORWARD);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            mover.MoveInDir(MovDir.BACK);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mover.MoveInDir(MovDir.LEFT);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            mover.MoveInDir(MovDir.RIGHT);
        }
    }
}
