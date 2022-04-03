using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;

public class StartGameButton : SceneLoader, IActivatable
{
    [SerializeField] private SaveBool _isFirstLoad;
    [SerializeField] private CutSceneLauncher _cutSceneLauncher;
    [SerializeField] private GoogleAdmobAverangeAd _averangeAd;
    [SerializeField] private string _gameSceneName = "Game", _firstLoadGameCutSceneName = "Backstory";


    public void Activate()
    {
        StartGame();
        return;

        _averangeAd.TryShow();

        _averangeAd.Ad.OnAdClosed += OnAdClosed;
        _averangeAd.Ad.OnAdFailedToLoad += OnAdFailedToLoad;

        void OnAdClosed<TEventArgs>(object sender, TEventArgs e)
        {
            OnAdWorked();
        }
        void OnAdFailedToLoad<TEventArgs>(object sender, TEventArgs e)
        {
            OnAdWorked();
        }
        void OnAdWorked()
        {
            _averangeAd.Ad.OnAdClosed -= OnAdClosed;
            _averangeAd.Ad.OnAdFailedToLoad -= OnAdFailedToLoad;
            StartGame();
        }
    }

    private void StartGame()
    {
        if (_isFirstLoad.Get()) _cutSceneLauncher.StartCutScene(_firstLoadGameCutSceneName, () => LoadScene(_gameSceneName));
        else LoadScene(_gameSceneName);
    }
}
