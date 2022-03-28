using System;
using UnityEngine;

public class UpdatingRealTimeSecond : MonoBehaviour
{
    [SerializeField] private UpdatingRealTime<int> _updatingRealTime;

    private void Start() => _updatingRealTime.Initialize(() => DateTime.Now.Second);


    //public void Updasting() => OnUpdating?.Invoke(DateTime.Now.Minute);
    //public void Updatidng() => OnUpdating?.Invoke(DateTime.Now.Hour);
    //public void Updatasing() => OnUpdating?.Invoke(DateTime.Now.Day);
    //public void Updatasing() => OnUpdating?.Invoke(DateTime.Now.DayOfWeek.ToString());
    //public void Updatasing() => OnUpdating?.Invoke(DateTime.Now.Month);
    //public void Updatasing() => OnUpdating?.Invoke(DateTime.Now.Year);
}
