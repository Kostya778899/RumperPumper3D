using System;
using UnityEngine;

public class UpdatingRealTimeMonth : MonoBehaviour
{
    [SerializeField] private UpdatingRealTime<int> _updatingRealTime;

    private void Start() => _updatingRealTime.Initialize(() => DateTime.Now.Month);
}
