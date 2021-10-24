using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int _time;
    private bool _canDecreaseCreationDelayFactor;
    private bool _canIncreaseSpeedFactor;

    private int DECREASE_MODUO = 15;
    private int INCREASE_MODUO = 10;

    void Awake()
    {
        SubscribeToActions();
    }

    private void OnDestroy()
    {
        UnSubscribeToActions();
    }

    private void SubscribeToActions()
    {
        Actions.StartGameAction += StartTimer;
    }

    private void UnSubscribeToActions()
    {
        Actions.StartGameAction -= StartTimer;
    }

    public void StartTimer()
    {
        StartCoroutine(Timer(120, () => Actions.TimeIsUpAction?.Invoke()));
    }

    private IEnumerator Timer(float time, Action action = null)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            _time = (int)(time - t);
            Actions.TimerChangedAction?.Invoke(_time);

            if (_time % DECREASE_MODUO == 0)
            {
                if (!_canDecreaseCreationDelayFactor)
                {
                    Actions.DecreaseCreationDelayFactorAction?.Invoke();
                    _canDecreaseCreationDelayFactor = true;
                }
            }
            else
            {
                _canDecreaseCreationDelayFactor = false;
            }

            if (_time % INCREASE_MODUO == 0)
            {
                if (!_canIncreaseSpeedFactor)
                {
                    Actions.IncreaseSpeedFactorAction?.Invoke();
                    _canIncreaseSpeedFactor = true;
                }
            }
            else
            {
                _canIncreaseSpeedFactor = false;
            }

            yield return new WaitForEndOfFrame();
        }
        action?.Invoke();
    }

    #region Getters And Setters
    public int TimeReference
    {
        get { return _time; }
    }
    #endregion
}
