using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class States
{
    public enum GameState { Menu, Play, Win, Pause };
    public static GameState GameStateReference;

    public enum ItemType { Book, BoxingGlow, Coin, Diamond, Energy };
}
