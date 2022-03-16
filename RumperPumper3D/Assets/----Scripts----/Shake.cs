using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour
{
    [SerializeField] private float _duration = 1f, _strength = 1f;
    [SerializeField] private int _vibrato = 10;
    [SerializeField] private Ease _ease = Ease.OutCirc;
    [SerializeField] private Vector3 _defaultPosition = Vector3.zero;


    public void Shake_() =>
        transform.DOShakePosition(_duration, _strength, _vibrato, 0f).SetEase(_ease)
        .OnComplete(() => transform.position = _defaultPosition);
}
