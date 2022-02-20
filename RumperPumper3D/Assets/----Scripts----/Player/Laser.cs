using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private MousePositionInWorldByRaycastInput _mouseRaycastInput;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LineRenderer _graphicsLine;
    [SerializeField] private Transform _hitEffect;

    [Min(0f), SerializeField] private float _lineDistance = 100f;


    public void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward * _lineDistance, out RaycastHit hit))
        {
            _graphicsLine.SetPosition(1, hit.point);

            _hitEffect.transform.position = hit.point;
            _hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else
        {
            _graphicsLine.SetPosition(1, transform.forward * _lineDistance);
        }
    }

    private void Start()
    {
        _graphicsLine.positionCount = 2;
        _graphicsLine.SetPosition(0, _shootPoint.position);
    }
    private void Update()
    {
        Shoot();
    }
}
