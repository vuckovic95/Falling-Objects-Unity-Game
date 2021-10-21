using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int _time;
    void Start()
    {
        SubscribeToActions();

        //privremeno
        StartTimer();
    }

    private void SubscribeToActions()
    {
        Actions.StartGameAction += StartTimer;
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
