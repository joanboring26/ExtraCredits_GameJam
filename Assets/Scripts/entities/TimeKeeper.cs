﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class TimeKeeper
{
    static List<Character> characters;

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

    public static void NextTurn()
    {
        foreach (var character in characters)
        {
            if (character == null)
                continue;
            character.CharacterUpdate();
        }
    }
}
