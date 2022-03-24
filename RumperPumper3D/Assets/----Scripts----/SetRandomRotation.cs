using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CMath;

public class SetRandomRotation : MonoBehaviour, IUpdatable
{
    [SerializeField] private Vector3 _minRotation = Vector3.zero, _maxRotation = Vector3.one;
    [SerializeField] private bool _updatingOnAwake = false;


    public void Updating()
    {
        transform.rotation = Quaternion.Euler(
            UnityEngine.Random.Range(_minRotation.x, _maxRotation.x),
            UnityEngine.Random.Range(_minRotation.y, _maxRotation.y),
            UnityEngine.Random.Range(_minRotation.z, _maxRotation.z));
    }

    private void Awake() { if (_updatingOnAwake) Updating(); }
}
