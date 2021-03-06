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

    private string ENTRY_SCENE = "EntryScene";

    private void Awake()
    {
        SetAppSettings();
    }

    void Start()
    {
        SubscribeToActions();
        Actions.StartGameAction?.Invoke();
    }

    private void OnDestroy()
    {
        UnSubscribeToActions();
    }

    private void SubscribeToActions()
    {
        Actions.TimeIsUpAction += GameCompleted;
        Actions.StartGameAction += () => { Time.timeScale = 1; };
    }

    private void UnSubscribeToActions()
    {
        Actions.TimeIsUpAction += GameCompleted;
        Actions.StartGameAction += () => { Time.timeScale = 1; };
    }

    private void GameCompleted()
    {
        MMVibrationManager.Haptic(HapticTypes.Success);
        ParameterSet parameters = new ParameterSet();
        parameters.Add("Score", _dataManager.GetScore);
        parameters.Add("HighScore", _dataManager.GetHighScore);

        _dialogService.OpenDialog<GameFinishedDialog>(parameters, GameFinishedDialogCloseCallback);
    }

    private void GameFinishedDialogCloseCallback(DialogResult result, ParameterSet parameters)
    {
        if (result is DialogResult.Quit)
            Actions.ChangeSceneAction?.Invoke(0);
        else if (result is DialogResult.Retry)
            Actions.StartGameAction?.Invoke();
    }

    private void SetAppSettings()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
