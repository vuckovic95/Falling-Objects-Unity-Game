using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;
using System;

public class GameFinishedDialog : DialogBase
{
    [BoxGroup("Buttons")]
    [SerializeField]
    private Button _retryBtn;
    [BoxGroup("Buttons")]
    [SerializeField]
    private Button _quitBtn;

    [BoxGroup("Text Properties")]
    [SerializeField]
    private TextMeshProUGUI _scoreTxt;
    [BoxGroup("Text Properties")]
    [SerializeField]
    private TextMeshProUGUI _highScoreTxt;

    private void Start()
    {
        _retryBtn.onClick.AddListener(() => CloseDialog(DialogResult.Retry, null));
        _quitBtn.onClick.AddListener(() => CloseDialog(DialogResult.Quit, null));
    }

    public override void OnDialogOpened(ParameterSet parameters)
    {
        parameters.TryGetValue<int>("Score", out int score);
        parameters.TryGetValue<int>("HighScore", out int highScore);

        _scoreTxt.text = score.ToString();
        _highScoreTxt.text = highScore.ToString();
    }
}
