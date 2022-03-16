using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using CMath;

public class GameSpeedTuner : MonoBehaviour, CMath.IIncluded
{
    public UnityEvent<float> OnUpdateGameProgress;
    public UnityEvent<float> OnUpdateGameProgress01;
    public UnityEvent<float> OnUpdateGameSpeed;

    [SerializeField] private PauseGameChanger _pauseChanger;

    [SerializeField] private AnimationCurve _gameSpeedCurve;
    [SerializeField] private float _tuneGameSpeedDuration = 60f;

    private Sequence _sequence;
    private (float InStart, float InEnd) _speed;


    public void Activate() => _sequence.Play();
    public void DeActivate()
    {
        _sequence.Pause();
        OnUpdateGameProgress.Invoke(0f);
    }

    private void Start()
    {
        _speed = (_gameSpeedCurve.GetFirstKey().value, _gameSpeedCurve.GetLastKey().value);

        _pauseChanger.OnPause += DeActivate;
        _pauseChanger.OnResume += Activate;

        OnUpdateGameProgress.AddListener((float value) => OnUpdateGameProgress01.Invoke(Mathf.InverseLerp(_speed.InStart, _speed.InEnd, value)));
        OnUpdateGameProgress01.AddListener((float value) => OnUpdateGameSpeed?.Invoke(_gameSpeedCurve.Evaluate(value)));

        _sequence = DOTween.Sequence();
        _sequence.Append(DOTween.To(() => _speed.InStart, x => OnUpdateGameProgress.Invoke(x), _speed.InEnd, _tuneGameSpeedDuration).SetEase(Ease.Linear));
    }
}
