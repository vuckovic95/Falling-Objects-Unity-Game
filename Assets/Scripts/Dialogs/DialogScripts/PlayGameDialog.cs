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
        Actions.BonusPickedAction += TurnOnBonusPanel;
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

    private void TurnOnBonusPanel(int bonus)
    {
        Vector2 start = new Vector2(-Screen.width, Screen.height / 4);
        Vector2 end = new Vector2(-Screen.width / 3, Screen.height / 4);
        _bonusTxt.text = bonus.ToString();

        _bonusPanel.SetActive(true);

        StartCoroutine(LerpV2Position(_bonusPanel.GetComponent<RectTransform>(), start, end, .2f, () =>
        {
            StartCoroutine(WaitForSeconds(1f, () =>
            {
                StartCoroutine(LerpV2Position(_bonusPanel.GetComponent<RectTransform>(), end, start, .2f, () => { _bonusPanel.SetActive(false); }));
            }));
        }));
    }

    public override void OnDialogOpened(ParameterSet parameters)
    {

    }

    private IEnumerator LerpV2Position(RectTransform tr, Vector2 start, Vector2 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            Vector2 x = Vector2.Lerp(start, end, t);
            tr.localPosition = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        tr.localPosition = end;

        action?.Invoke();
    }

    private IEnumerator WaitForSeconds(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
