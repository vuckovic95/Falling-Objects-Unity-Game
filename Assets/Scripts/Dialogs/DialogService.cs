using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class DialogService : MonoBehaviour
{
    [SerializeField] 
    Transform _parent;

    Dictionary<Type, DialogBase> _dialogs = new Dictionary<Type, DialogBase>();

    private void Awake()
    {
        PopulateDialogs();

        ParameterSet set = new ParameterSet();
        OpenDialog<PlayGameDialog>(set, null);
    }

    private void OnDestroy()
    {
        _dialogs.Clear();
    }

    public void OpenDialog<T>(ParameterSet parameters, Action<DialogResult, ParameterSet> callback) where T : DialogBase
    {
        if (_dialogs.ContainsKey(typeof(T)))
        {
            _dialogs[typeof(T)].Initialize(callback);
            _dialogs[typeof(T)].gameObject.SetActive(true);
            _dialogs[typeof(T)].OnDialogOpened(parameters);
        }
    }

    private void PopulateDialogs()
    {
        for (int i = 0; i < _parent.childCount; i++)
        {
            DialogBase temp = _parent.GetChild(i).GetComponent<DialogBase>();
            if (temp != null)
            {
                temp.gameObject.SetActive(false);
                _dialogs.Add(temp.GetType(), temp);
            }
        }
    }

    public void TurnOfSelectedDialog(string scriptName)
    {
        foreach (Transform tr in _parent)
        {
            if(tr.gameObject.name == scriptName)
            {
                tr.gameObject.SetActive(false);
            }
        }
    }

    public void TurnOffAllDialogs()
    {
        foreach(Transform tr in _parent)
        {
            tr.gameObject.SetActive(false);
        }
    }

    public DialogBase GetDialog<T>() where T : DialogBase
    {
        return _dialogs[typeof(T)];
    }
}
