using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

[Serializable]
public class UpdatingRealTime<T> : IUpdatable
{
    public UnityEvent<T> OnUpdating;

    [SerializeField] private bool _updatingOnInitialize = false;

    private CGetter<T> _timeGetter;


    public void Initialize(CGetter<T> timeGetter)
    {
        _timeGetter = timeGetter;
        if (_updatingOnInitialize) Updating();
    }
    public void Updating() => OnUpdating?.Invoke(_timeGetter.Invoke());
}
