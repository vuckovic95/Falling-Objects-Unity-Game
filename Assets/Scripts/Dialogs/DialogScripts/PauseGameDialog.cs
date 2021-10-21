using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;
using System;

public class PauseGameDialog : DialogBase
{
    [BoxGroup("Buttons")]
    [SerializeField]
    private Button _continueBtn;
    [BoxGroup("Buttons")]
    [SerializeField]
    private Button _quitBtn;

    private void Start()
    {
        _continueBtn.onClick.AddListener(() => CloseDialog(DialogResult.Continue, null));
        _quitBtn.onClick.AddListener(() => CloseDialog(DialogResult.Quit, null));
    }

    public override void OnDialogOpened(ParameterSet parameters)
    {

    }
}
