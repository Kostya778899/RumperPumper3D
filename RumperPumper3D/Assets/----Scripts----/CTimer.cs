using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CTimer : MonoBehaviour
{
    [SerializeField] private bool _enabledOnStart = true;
    [SerializeField] private NullableVar<float> _time = new NullableVar<float>(30f);
    [Min(0f), SerializeField] private float _stepSize = 1f;
    [SerializeField] private bool _usingRealtime = false;

    [SerializeField] private UnityEvent<float> _onStep;
    [SerializeField] private UnityEvent<float> _onStep01;
    [SerializeField] private UnityEvent _onPinpoint;
    [SerializeField] private UnityEvent _onCompletion;


    public void Pinpoint() => StartCoroutine(PinpointCorutine());
    public IEnumerator PinpointCorutine()
    {
        _onPinpoint?.Invoke();

        for (float t = 0f; _time.IsNull || t < _time.Value; t += _stepSize)
        {
            _onStep?.Invoke(t);
            _onStep01?.Invoke(Mathf.InverseLerp(0f, _time.Value, t));

            if (t >= float.MaxValue) t = 0f;
            if (_usingRealtime) yield return new WaitForSecondsRealtime(_stepSize);
            else yield return new WaitForSeconds(_stepSize);
        }
        _onStep01?.Invoke(1f);
        _onCompletion?.Invoke();
    }

    private void Start() { if (_enabledOnStart) Pinpoint(); }
}
