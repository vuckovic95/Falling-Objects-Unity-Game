using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataManager : MonoBehaviour
{
    #region Properties
    private int _score;
    private int _highScore;

    private string HIGHSCORE_STRING = "HighScore";
    private string SCORE_STRING = "Score";
    #endregion

    #region Private Functions
    private void Awake()
    {
        _score = PlayerPrefs.GetInt(SCORE_STRING, 0);
        _highScore = PlayerPrefs.GetInt(HIGHSCORE_STRING, 0);

        SubscribeToActions();
    }

    private void OnDestroy()
    {
        UnSubscribeToActions();
    }

    private void SubscribeToActions()
    {
        Actions.ItemPickedAction += IncreaseScoreOnItemPicked;
        Actions.StartGameAction += ResetScore;
        Actions.BonusPickedAction += IncreaseScoreOnBonusPicked;
        Actions.TimeIsUpAction += CheckIfIsHighScore;
    }

    private void UnSubscribeToActions()
    {
        Actions.ItemPickedAction -= IncreaseScoreOnItemPicked;
        Actions.StartGameAction -= ResetScore;
        Actions.BonusPickedAction -= IncreaseScoreOnBonusPicked;
        Actions.TimeIsUpAction -= CheckIfIsHighScore;
    }

    private void SaveHighScore(int newHighScore)
    {
        _highScore = newHighScore;
        PlayerPrefs.SetInt(HIGHSCORE_STRING, _highScore);

        Actions.NewHighScoreAction?.Invoke();
    }
    #endregion

    #region Public Functions
    public void IncreaseScore(int scoreToIncrease)
    {
        _score += scoreToIncrease;
    }

    public void IncreaseScoreOnItemPicked(Item item)
    {
        _score += item.Object.GetPoints;

        Actions.IncreaseScoreAction?.Invoke(_score);
    }

    private void IncreaseScoreOnBonusPicked(int bonus)
    {
        _score += bonus;

        Actions.IncreaseScoreAction?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;

        Actions.IncreaseScoreAction?.Invoke(_score);
    }

    public void CheckIfIsHighScore()
    {
        if(_score > _highScore)
        {
            SaveHighScore(_score);
        }
    }
    #endregion

    #region Getters
    public int GetScore
    {
        get { return _score; }
    }

    public int GetHighScore
    {
        get { return _highScore; }
    }
    #endregion
}
