using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerInput : MonoBehaviour
{
    Character player = null;
    Character charInTheWay;
    public static bool isKillingMove = false;

    private void Awake()
    {
        player = GetComponent<Character>();
    }

    void Update()
    {
        if (player == null)
            return;

        if (
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W))
        {
            player.currDir = MovDir.FORWARD;
        }
        else if(
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.S))
        {
            player.currDir = MovDir.BACK;
        }
        else if(
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.A))
        {
            player.currDir = MovDir.LEFT;
        }
        else if(
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.D))
        {
            player.currDir = MovDir.RIGHT;
        }
        else
        {
            return;
        }
        // check if the player is holding shift to do a kill
        if (Input.GetKey(KeyCode.LeftShift) ||
            Input.GetKey(KeyCode.RightShift))
        {
            isKillingMove = true;
        }
        StartCoroutine(UpdateAllCharacters());
    }

    IEnumerator UpdateAllCharacters()
    {
        Mover.MoveCharacter(player, out charInTheWay, true, player.currDir, Physics.DefaultRaycastLayers);
        yield return new WaitForFixedUpdate();
        TimeKeeper.NextTurn();

        player.currDir = MovDir.NONE;
        charInTheWay = null;
        isKillingMove = false;
    }
}
