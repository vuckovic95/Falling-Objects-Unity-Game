﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class PlayGameDialog : DialogBase
{
    [Inject]
    DialogService _dialogService;

    [BoxGroup("Buttons")]
    [SerializeField]
    private Button _pauseBtn;

    [BoxGroup("Text Fields")]
    [SerializeField]
    private TextMeshProUGUI _timeTxt;

    [BoxGroup("Arrows")]
    [SerializeField]
    private Button _rightArrow;
    [BoxGroup("Arrows")]
    [SerializeField]
    private Button _leftArrow;


    private void Start()
    {
        SubscribeToActions();

        _pauseBtn.onClick.AddListener(PauseClicked);
    }

    private void SubscribeToActions()
    {
        Actions.TimerChangedAction += UpdateTime;
    }

    private void PauseClicked()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            _dialogService.OpenDialog<PauseGameDialog>(null, PauseGameCallback);
        }
    }

    private void PauseGameCallback(DialogResult result, ParameterSet parameters)
    {
        if(result is DialogResult.Quit)
        {
            Actions.QuitGameAction?.Invoke();
        }
        PauseClicked();
    }

    private void UpdateTime(int time)
    {
        _timeTxt.text = time.ToString();
    }

    public override void OnDialogOpened(ParameterSet parameters)
    {

    }
}