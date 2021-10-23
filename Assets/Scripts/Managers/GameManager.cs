using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    [Inject]
    DialogService _dialogService;
    [Inject]
    DataManager _dataManager;

    private void Awake()
    {
        SetAppSettings();
    }

    void Start()
    {
        SubscribeToActions();
    }

    private void SubscribeToActions()
    {
        Actions.TimeIsUpAction += GameCompleted;
        Actions.StartGameAction += () => { Time.timeScale = 1; };
    }

    private void GameCompleted()
    {
        ParameterSet parameters = new ParameterSet();
        parameters.Add("Score", _dataManager.GetScore);
        parameters.Add("HighScore", _dataManager.GetHighScore);
        Time.timeScale = 0;

        _dialogService.OpenDialog<GameFinishedDialog>(parameters, GameFinishedDialogCloseCallback);
    }

    private void GameFinishedDialogCloseCallback(DialogResult result, ParameterSet parameters)
    {
        if (result is DialogResult.Quit)
            Actions.QuitGameAction?.Invoke();
        else if (result is DialogResult.Retry)
            Actions.StartGameAction?.Invoke();
    }

    private void SetAppSettings()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
