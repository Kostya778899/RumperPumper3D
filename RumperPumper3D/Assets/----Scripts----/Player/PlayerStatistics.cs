using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    [SerializeField] private Score _currentScore, _highScore, _coinsScore;


    public void UpdatingScoresTexts()
    {
        _currentScore.UpdatingTexts();
        _highScore.UpdatingTexts();
    }
    public void TryUpdatingScores(int value) => _currentScore.TrySetScore(value);

    public void AddToCurrentScore(int value) => TryUpdatingScores(_currentScore.Score_ + value);

    public void AddCoins(int value) => _coinsScore.TrySetScore(_coinsScore.Score_ + value);

    public void OnEndSession() => _highScore.TrySetScore(_currentScore.Score_);
}
