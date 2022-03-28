using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class RealTime : MonoBehaviour, IUpdatable
{
    public UnityEvent<int> OnUpdatingDateInt;

    [SerializeField] private DataTypes _dateType;
    [SerializeField] private bool _updatingOnStart = false;

    private enum DataTypes { Second, Minute, Hour, Day, Month, Year }


    public void Updating() => OnUpdatingDateInt?.Invoke(GetCurrentDate());

    private void Start() { if (_updatingOnStart) Updating(); }

    private int GetCurrentDate() => (int)typeof(DateTime).GetProperty("Now").PropertyType.GetProperty(_dateType.ToString()).GetValue(DateTime.Now);
}
