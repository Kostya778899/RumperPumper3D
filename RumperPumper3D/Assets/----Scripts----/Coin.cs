using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class Coin : MonoBehaviour, IDeActivatableByLaser
{
    [SerializeField] private CoinSettings _settings;
    [SerializeField] private UnityEvent _onDeActivate;


    public void DeActivate()
    {
        _onDeActivate?.Invoke();
        if (transform.parent) Instantiate(_settings.DestroyEffectPrefab, transform.parent);
        else Instantiate(_settings.DestroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
