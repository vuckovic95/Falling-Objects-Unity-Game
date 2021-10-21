using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogBase : MonoBehaviour
{
    private event Action<DialogResult, ParameterSet> RequestClose;

    public void Initialize(Action<DialogResult, ParameterSet> callback)
    {
        RequestClose = callback;
    }

    public abstract void OnDialogOpened(ParameterSet parameters);

    protected void CloseDialog(DialogResult result, ParameterSet parameters)
    {
        gameObject.SetActive(false);
        RequestClose?.Invoke(result, parameters);
    }
}
