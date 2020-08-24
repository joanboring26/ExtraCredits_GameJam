using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class TimeKeeper
{
    static List<Character> characters;
    public static int CharactersCount
    {
        get {
            return characters.Count;
        }
    }

    public static void Register(Character character)
    {
        if (characters == null)
            characters = new List<Character>();
        characters.Add(character);
    }

    public static void Deregister(Character character)
    {
        if (characters == null)
            return; 
        characters.Remove(character);
    }

    public static void NextTurn(int i)
    {        
        Character character = characters[i];
        if (character == null)
            return;
        character.CharacterUpdate();
              
    }
}
