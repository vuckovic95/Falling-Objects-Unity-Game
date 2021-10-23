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
    [Inject]
    KeyHandler _keyHandler;

    [BoxGroup("Buttons")]
    [SerializeField]
    private Button _continueBtn;
    [BoxGroup("Buttons")]
    [SerializeField]
    private Button _quitBtn;

    private string ENTRY_SCENE = "EntryScene";
    private string GAMEPLAY_SCENE = "GamePlayScene";

    private void Start()
    {
        _continueBtn.onClick.AddListener(() => CloseDialog(DialogResult.Continue, null));
        _quitBtn.onClick.AddListener(() => CloseDialog(DialogResult.Quit, null));
    }

    private void OnEnable()
    {
        _keyHandler.Bind(KeyCode.Escape, () => CloseDialog(DialogResult.Continue, null));
    }

    private void OnDisable()
    {
        _keyHandler.UnBind(KeyCode.Escape, () => CloseDialog(DialogResult.Continue, null));
    }

    public override void OnDialogOpened(ParameterSet parameters)
    {
        //do nothing
    }
}
