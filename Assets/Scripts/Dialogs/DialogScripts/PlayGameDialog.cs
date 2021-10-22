using System.Collections;
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

    [BoxGroup("Bonus Properties")]
    [SerializeField]
    private GameObject _bonusPanel;
    [BoxGroup("Bonus Properties")]
    [SerializeField]
    private TextMeshProUGUI _bonusTxt;

    [BoxGroup("Buttons")]
    [SerializeField]
    private Button _pauseBtn;

    [BoxGroup("Text Fields")]
    [SerializeField]
    private TextMeshProUGUI _timeTxt;
    [BoxGroup("Text Fields")]
    [SerializeField]
    private TextMeshProUGUI _scoreTxt;

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
        Actions.IncreaseScoreAction += UpdateScore;
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

    private void UpdateScore(int score)
    {
        _scoreTxt.text = score.ToString();
    }

    public override void OnDialogOpened(ParameterSet parameters)
    {

    }
}
