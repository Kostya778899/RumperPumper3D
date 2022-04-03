using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;
using UnityEngine.Events;

public class Event : MonoBehaviour, IActivatable
{
    public UnityEvent Event_;

    public void Activate() => Event_?.Invoke();
}
