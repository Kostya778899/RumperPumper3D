using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    //[SerializeField] private PlayerKill _kill;
    [SerializeField] private Score _currentScore, _highScore;


    public void UpdatingScoresTexts()
    {
        _currentScore.UpdatingTexts();
        _highScore.UpdatingTexts();
    }
    public void TryUpdatingScores(int value) => _currentScore.TrySetScore(value);

    public void AddToCurrentScore(int value) => TryUpdatingScores(_currentScore.Value + value);

    //private void Start() => _kill.OnKill.AddListener(OnEndSession);

    public void OnEndSession() => _highScore.TrySetScore(_currentScore.Value);
}
