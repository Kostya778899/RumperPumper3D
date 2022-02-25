using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collision> _onCollosion;
    [SerializeField] private UnityEvent<LetDefault, Collision> _onCollosionWithLet;


    private void OnCollisionEnter(Collision collision)
    {
        _onCollosion?.Invoke(collision);
        if (collision.gameObject.TryGetComponent<LetDefault>(out LetDefault let)) _onCollosionWithLet?.Invoke(let, collision);
    }
}
