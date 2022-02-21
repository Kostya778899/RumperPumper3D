using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyableByLaser : MonoBehaviour
{
    [SerializeField] private UnityEvent<RaycastHit> _onDestroyByLaser;


    public void Destroy(RaycastHit hit)
    {
        _onDestroyByLaser?.Invoke(hit);
        Destroy(this.gameObject);
    }
}
