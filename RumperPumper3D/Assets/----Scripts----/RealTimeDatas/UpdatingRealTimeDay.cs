using System;
using UnityEngine;

public class UpdatingRealTimeDay : MonoBehaviour
{
    [SerializeField] private UpdatingRealTime<int> _updatingRealTime;

    private void Start() => _updatingRealTime.Initialize(() => DateTime.Now.Day);
}
