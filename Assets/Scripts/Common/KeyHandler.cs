using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Key Handler i Key listener su radjeni po ugledu na sam Windows i Windows app gde imamo globalni servis koji se bavi dogadjajima kad pritisnemo neko dugme (umesto da imamo vise razlicitih klasa, 
/// svaka ima Update i proverava konstantno da li je korisnik kliknuo nesto). Optimizovanije i lakse za snalazenje na vecim projektima.
/// </summary>
public class KeyHandler
{
    public Dictionary<KeyCode, List<Action>> KeyDictionary = new Dictionary<KeyCode, List<Action>>();
    public HashSet<KeyCode> Keys = new HashSet<KeyCode>();

    public void Bind(KeyCode code, Action action)
    {
        if(!KeyDictionary.ContainsKey(code))
            KeyDictionary.Add(code, new List<Action>());

        KeyDictionary[code].Add(action);

        Keys.Add(code);
    }

    public void UnBind(KeyCode code, Action action)
    {
        if (KeyDictionary.ContainsKey(code))
        {
            KeyDictionary[code].Remove(action);
        }
    }
}
