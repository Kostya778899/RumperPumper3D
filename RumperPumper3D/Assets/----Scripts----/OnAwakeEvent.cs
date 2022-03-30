using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAwakeEvent : MonoBehaviour
{
    public UnityEvent Event;

    private void Awake() => Event?.Invoke();
}
