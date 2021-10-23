using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class KeyListener : MonoBehaviour
{
    [Inject]
    KeyHandler _keyHandler;

    void Update()
    {
        for (int i = _keyHandler.Keys.Count - 1; i >= 0; i--)
        {
            KeyCode code = _keyHandler.Keys.ToList()[i];
            if (Input.GetKeyDown(code))
            {
                if (_keyHandler.KeyDictionary.ContainsKey(code))
                {
                    List<Action> actions = _keyHandler.KeyDictionary[code];
                    for (int j = actions.Count - 1; j >= 0; j--)
                    {
                        actions[j]?.Invoke();
                    }
                }
            }
        }
    }
}
