using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;

public class CarLet : Let, IDeActivatable
{
    [SerializeField] private CarLetSettings _settings;


    public void DeActivate()
    {
        Instantiate(_settings.DestroyParticles, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
