using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CTimer : MonoBehaviour
{
    [SerializeField] private bool _enabledOnStart = true;
    [SerializeField] private NullableVar<float> _time = new NullableVar<float>(30f);
    [SerializeField] private float _stepSize = 1f;

    [SerializeField] private UnityEvent<float> _onStep;
    [SerializeField] private UnityEvent _onCompletion;


    public void Pinpoint() => StartCoroutine(PinpointCorutine());
    public IEnumerator PinpointCorutine()
    {
        for (float t = 0f; !_time.IsNull && t < _time.Value; t += _stepSize)
        {
            _onStep?.Invoke(t);

            if (t >= float.MaxValue) t = 0f;
            yield return new WaitForSeconds(_stepSize);
        }

        _onCompletion?.Invoke();
    }

    private void Start() { if (_enabledOnStart) Pinpoint(); }
}
