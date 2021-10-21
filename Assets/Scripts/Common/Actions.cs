using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions
{
    public static Action StartGameAction;
    public static Action QuitGameAction;
    public static Action TimeIsUpAction;
    public static Action<int> TimerChangedAction;
    public static Action<int> ItemPickedAction;
    public static Action HoldLeftArrowAction;
    public static Action HoldRightArrowAction;
}
