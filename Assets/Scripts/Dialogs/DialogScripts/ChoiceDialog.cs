using TMPro;
using UnityEngine;

public class ChoiceDialog : DialogBase
{
    //[SerializeField] TMP_Text _text;
    //[SerializeField] ButtonWidget _trueButton;
    //[SerializeField] ButtonWidget _falseButton;

    //private ParameterSet _parameterSet;

    //private void Start()
    //{
    //    _falseButton.OnClick += () => CloseDialog(DialogResult.Abort, _parameterSet);
    //    _trueButton.OnClick += () => CloseDialog(DialogResult.OK, _parameterSet);
    //}

    public override void OnDialogOpened(ParameterSet parameters)
    {
        //parameters.TryGetValue<string>("Yes", out string trueText);
        //parameters.TryGetValue<string>("No", out string falseText);
        //parameters.TryGetValue<string>("Message", out string message);
        //parameters.TryGetValue<Card>("Card", out Card card);

        //_parameterSet = parameters;

        //_text.text = message;
        //_trueButton.Text = trueText;
        //_falseButton.Text = falseText;
    }
}
