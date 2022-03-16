using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StateMachine<T>
{
    public UnityEvent<State<T>> OnChangeState;
    public State<T>[] States;

    [SerializeField] private int _currentStateIndex = 0;

    [System.Serializable]
    public struct State<T0>
    {
        public string Name;
        public T0 Value;
        public UnityEvent<T0> OnEnter, OnExit;


        public void Enter() => OnEnter?.Invoke(Value);
        public void Exit() => OnExit?.Invoke(Value);
    }


    public StateMachine(State<T>[] states, int currentState)
    {
        States = states;
        _currentStateIndex = currentState;
    }

    public int GetCurrentStateIndex() => _currentStateIndex;

    public State<T> GetCurrentState() => States[_currentStateIndex];

    public void SetCurrentStateIndex(int newcurrentStateIndex)
    {
        States[_currentStateIndex].Exit();
        States[newcurrentStateIndex].Enter();
        _currentStateIndex = newcurrentStateIndex;

        OnChangeState?.Invoke(GetCurrentState());
    }

    public bool TrySetCurrentStateIndex(int newcurrentStateIndex)
    {
        bool newStateNotMatchOld = newcurrentStateIndex != _currentStateIndex;
        if (newStateNotMatchOld) SetCurrentStateIndex(newcurrentStateIndex);
        return newStateNotMatchOld;
    }
    public bool TrySetCurrentStateIndex(string newcurrentStateName)
    {
        for (int i = 0; i < States.Length; i++) if (States[i].Name == newcurrentStateName) return TrySetCurrentStateIndex(i);
        return false;
    }
}
