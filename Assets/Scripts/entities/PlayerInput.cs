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

        if (Input.GetKeyDown(KeyCode.UpArrow))
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
        for (int i = TimeKeeper.CharactersCount - 1; i >= 0; i--)
        {
            TimeKeeper.NextTurn(i);
            yield return new WaitForFixedUpdate();
        }

        player.currDir = MovDir.NONE;
        charInTheWay = null;
        isKillingMove = false;
    }
}
