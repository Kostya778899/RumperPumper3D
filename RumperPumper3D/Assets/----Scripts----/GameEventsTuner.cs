using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsTuner : MonoBehaviour
{
    //[SerializeField] private GameSpeedTuner _speedTuner;

    [SerializeField] private StateMachine<float> _states;


    //private void Start()
    //{
    //    _speedTuner.OnUpdateGameProgress.AddListener(TrySetNextState);
    //}

    public void TrySetNextState(float progress)
    {
        int nextStateIndex = _states.GetCurrentStateIndex() + 1;
        if (nextStateIndex >= _states.States.Length) return;

        if (_states.States[nextStateIndex].Value <= progress) _states.SetCurrentStateIndex(nextStateIndex);
    }
}
