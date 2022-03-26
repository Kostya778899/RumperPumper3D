using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class CutScene : MonoBehaviour, IActivatable
{
    public UnityEvent OnStart;
    public UnityEvent OnComplete;


    public async void Activate()
    {
        OnStart?.Invoke();
        await Task.Delay(5000);
        OnComplete?.Invoke();
    }
}
