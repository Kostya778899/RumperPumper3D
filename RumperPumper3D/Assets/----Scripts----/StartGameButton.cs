using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;

public class StartGameButton : SceneLoader, IActivatable
{
    [SerializeField] private SaveBool _isFirstLoad;
    [SerializeField] private CutSceneLauncher _cutSceneLauncher;
    [SerializeField] private string _gameSceneName = "Game", _firstLoadGameCutSceneName = "Backstory";


    public void Activate()
    {
        if (_isFirstLoad.Get()) _cutSceneLauncher.StartCutScene(_firstLoadGameCutSceneName, () => LoadScene(_gameSceneName));
        else LoadScene(_gameSceneName);
    }
}
