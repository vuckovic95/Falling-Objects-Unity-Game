using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;
using System;
using Spine.Unity;

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

    [BoxGroup("Girl")]
    [SerializeField]
    private GameObject _girl;
    [BoxGroup("Girl")]
    [SpineAnimation]
    [SerializeField]
    private string _wellDoneAnimationName;

    private Vector3 _startGirlPosition;
    private Vector3 _endGirlPosition;

    private void Awake()
    {
        SetStartAndEndGirlPositions();
    }

    private void Start()
    {
        _retryBtn.onClick.AddListener(() => CloseDialog(DialogResult.Retry, null));
        _quitBtn.onClick.AddListener(() => CloseDialog(DialogResult.Quit, null));
    }

    private void SetStartAndEndGirlPositions()
    {
        _endGirlPosition = new Vector3(-Screen.width / 3, -Screen.height / 1.5f, -9000);
        _startGirlPosition = new Vector3(-Screen.width / 3, -Screen.height, -9000);
    }

    public override void OnDialogOpened(ParameterSet parameters)
    {
        parameters.TryGetValue<int>("Score", out int score);
        parameters.TryGetValue<int>("HighScore", out int highScore);

        _scoreTxt.text = score.ToString();
        _highScoreTxt.text = highScore.ToString();
    }

    private void OnEnable()
    {
        Transform girlRect = _girl.transform;
        girlRect.localPosition = _startGirlPosition;

        Spine.AnimationState spineAnimationState = _girl.GetComponent<SkeletonAnimation>().AnimationState;
        spineAnimationState.AddAnimation(0, _wellDoneAnimationName, true, 0);

        StartCoroutine(LerpV3Position(girlRect, _startGirlPosition, _endGirlPosition, 1f));
    }

    private IEnumerator LerpV3Position(Transform tr, Vector3 start, Vector3 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            Vector3 x = Vector3.Lerp(start, end, t);
            tr.localPosition = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
        tr.localPosition = end;

        action?.Invoke();
    }
}
