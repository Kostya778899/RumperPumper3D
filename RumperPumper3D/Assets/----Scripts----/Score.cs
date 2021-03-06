using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [HideInInspector] public int Score_ { get; protected set; } = 0;

    protected const int _defaultValue = 0;

    [SerializeField] private UnityEvent<int> _onSetScore;
    [SerializeField] private UiText[] _texts;
    [SerializeField] private bool _updateCurrentValueIfLessNewValue = true;


    public void UpdatingTextsValueField(int? value = null)
    {
        if (!value.HasValue) value = Score_;
        foreach (var item in _texts) item.SetValueField(value.Value.ToString());
    }
    public void UpdatingTexts() => UpdatingTextsValueField();

    public virtual void SetScore(int value)
    {
        if (value >= 0) Score_ = value;
        else throw new Exception();
        _onSetScore?.Invoke(Score_);
    }
    public bool TrySetScore(int value)
    {
        bool canSetScoreToNewValue = value >= 0 && (_updateCurrentValueIfLessNewValue || Score_ < value);
        if (canSetScoreToNewValue) SetScore(value);
        return canSetScoreToNewValue;
    }
    public void TrySetScore_(int value) => TrySetScore(value);

    protected virtual void Awake() => _onSetScore.AddListener((int value) => UpdatingTextsValueField(value));
    private void Start() => SetScore(Score_);
}
