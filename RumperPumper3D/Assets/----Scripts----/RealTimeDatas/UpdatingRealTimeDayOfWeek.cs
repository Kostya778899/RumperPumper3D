using System;
using UnityEngine;

public class UpdatingRealTimeDayOfWeek : MonoBehaviour
{
    [SerializeField] private UpdatingRealTime<int> _updatingRealTime;

    private void Start() => _updatingRealTime.Initialize(() => DateTime.Now.Second);
}
