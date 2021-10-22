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
    public static Action<Item> ItemPickedAction;
    public static Action HoldLeftArrowAction;
    public static Action HoldRightArrowAction;
    public static Action DecreaseCreationDelayFactorAction;
    public static Action IncreaseSpeedFactorAction;
    public static Action NewHighScoreAction;
    public static Action<int> IncreaseScoreAction;
}
