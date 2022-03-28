using System;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class UpdatingRealTimeMinute : MonoBehaviour
{
    [SerializeField] private UpdatingRealTime<int> _updatingRealTime;

    private void Start() => _updatingRealTime.Initialize(() => DateTime.Now.Minute);
}
