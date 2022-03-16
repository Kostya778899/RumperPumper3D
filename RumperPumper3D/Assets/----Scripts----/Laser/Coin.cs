using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class Coin : MonoBehaviour, IDeActivatable
{
    [SerializeField] private UnityEvent _onDeActivate;


    public void DeActivate()
    {
        _onDeActivate?.Invoke();
        Destroy(this.gameObject);
    }
}
