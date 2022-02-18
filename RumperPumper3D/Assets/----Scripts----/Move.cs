using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private MoveSettings _settings;


    public void Moving() => StartCoroutine(MovingCorutine(null));
    public void Moving(Action callback) => StartCoroutine(MovingCorutine(callback));
    public IEnumerator MovingCorutine(Action callback = null)
    {
        Vector3 newLocalPosition = Vector3.zero;
        for (float t = 0; ; t += Time.deltaTime)
        {
            while (!_settings.IsMoving) yield return null;

            newLocalPosition = _settings.Direction * _settings.Speed * t;
            if (newLocalPosition.IsMore(_settings.Distance)) break;
            transform.position += newLocalPosition;
            yield return null;
        }

        callback?.Invoke();
    }
}
