using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class Coin : MonoBehaviour, IDeActivatableByLaser
{
    [SerializeField] private CoinSettings _settings;
    [Min(0f), SerializeField] private float _deActivateEffectSizeCoefficient = 1f;
    [SerializeField] private UnityEvent _onDeActivate;


    public void DeActivate()
    {
        _onDeActivate?.Invoke();
        SpawnDeActivateEffect().transform.localScale *= _deActivateEffectSizeCoefficient;
        Destroy(this.gameObject);
    }

    private GameObject SpawnDeActivateEffect()
    {
        if (transform.parent) return Instantiate(_settings.DestroyEffectPrefab, transform.parent);
        return Instantiate(_settings.DestroyEffectPrefab, transform.position, Quaternion.identity);
    }
}
