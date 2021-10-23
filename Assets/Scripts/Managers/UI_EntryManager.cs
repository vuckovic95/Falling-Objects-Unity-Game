using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class UI_EntryManager : MonoBehaviour
{
    [Inject]
    DataManager _dataManager;
    [Inject]
    SceneSwitcher _sceneSwitcher;

    [SerializeField]
    private Button _playBtn;

    [SerializeField]
    private Transform _highScorePanel;
    [SerializeField]
    private TextMeshProUGUI _highScoreTxt;

    private string GAMEPLAY_SCENE = "GamePlayScene";
    private int _score;

    void Start()
    {
        _playBtn.onClick.AddListener(() => { Actions.ChangeSceneAction?.Invoke(1);});
    }

    private void OnEnable()
    {
        SetHighScore();
    }

    private void SetHighScore()
    {
        _score = PlayerPrefs.GetInt("HighScore");

        if (_score == 0)
            _highScorePanel.gameObject.SetActive(false);
        else
            _highScoreTxt.text = _score.ToString();
    }
}
