using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerInput : MonoBehaviour
{
    Character player = null;
    private void Awake()
    {
        player = GetComponent<Character>();
    }

    void Update()
    {
        if (player == null)
            return;
        MovDir direction;
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = MovDir.FORWARD;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = MovDir.BACK;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = MovDir.LEFT;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = MovDir.RIGHT;
        }
        else
        {
            return;
        }
        Character charInTheWay;
        Mover.MoveCharacter
            (player,
            out charInTheWay,
            direction,
            Physics.DefaultRaycastLayers);
    }
}
