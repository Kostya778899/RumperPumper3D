using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class PlayerKillWindow : MonoBehaviour, IIncluded
{
    [SerializeField] private UnityEvent<bool> _onChangeState;
    [SerializeField] private UnityEvent _onActivate;
    [SerializeField] private UnityEvent _onDeactivate;


    public void Activate()
    {
        _onChangeState?.Invoke(true);
        _onActivate?.Invoke();
    }
    public void DeActivate()
    {
        _onChangeState?.Invoke(false);
        _onDeactivate?.Invoke();
    }

    private void Start() => DeActivate();
}
