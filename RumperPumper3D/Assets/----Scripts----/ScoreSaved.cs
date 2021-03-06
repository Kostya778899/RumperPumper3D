using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSaved : Score
{
    [SerializeField] private string _saveKey = "Score";


    public override void SetScore(int value)
    {
        base.SetScore(value);
        Save();
    }

    protected override void Awake()
    {
        base.Awake();
        Load();
    }

    private void Save() => PlayerPrefs.SetInt(_saveKey, Score_);
    private void Load() => Score_ = PlayerPrefs.GetInt(_saveKey, _defaultValue);
}
