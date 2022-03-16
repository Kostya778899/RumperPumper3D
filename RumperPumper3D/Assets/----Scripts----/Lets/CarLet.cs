using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;

public class CarLet : Let, IDeActivatable
{
    [SerializeField] private CarLetSettings _settings;
    [SerializeField] private Vector3 _explosionLocalPosition = new Vector3(0f, 2f, 0f);


    public void DeActivate()
    {
        Instantiate(_settings.DestroyParticles, transform.position + _explosionLocalPosition, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
