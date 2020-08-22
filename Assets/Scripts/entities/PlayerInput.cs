using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerInput : MonoBehaviour
{
    Character player = null;

    Character charInTheWay;

    private void Awake()
    {
        player = GetComponent<Character>();
    }

    void Update()
    {
        player.currDir = MovDir.NONE;
        charInTheWay = null;
        if (player == null)
            return;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.currDir = MovDir.FORWARD;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.currDir = MovDir.BACK;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.currDir = MovDir.LEFT;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.currDir = MovDir.RIGHT;
        }
        else
        {
            return;
        }
        Mover.MoveCharacter(player, out charInTheWay, true, player.currDir, Physics.DefaultRaycastLayers);

    }
}
