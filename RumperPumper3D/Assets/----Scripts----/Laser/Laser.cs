using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CMath;

[RequireComponent(typeof(CTimer))]
public class Laser : MonoBehaviour
{
    [SerializeField] private LaserSettings _settings;
    [SerializeField] private MousePositionInWorldByRaycastInput _mouseRaycastInput;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LineRenderer _graphicsRay;
    [SerializeField] private Transform _hitEffect;

    [SerializeField] private Vector3 _defaultRotation;

    private CTimer _timer;
    private bool _canShoot = true;


    private bool TryShoot(RaycastHit targetHit)
    {
        if (!_canShoot) return false;
        _canShoot = false;

        var deActivatable = targetHit.collider.GetComponent<IDeActivatable>();

        Vector3 shootPoint = deActivatable is not null ? targetHit.transform.position : targetHit.point;
        Vector3 newRotation = Quaternion.LookRotation(transform.position - shootPoint).eulerAngles + _defaultRotation;
        transform.DORotate(newRotation, _settings.RotateDuration).OnComplete(() =>
        {
            _graphicsRay.SetPositions(_shootPoint.position);

            _graphicsRay.SetPositionSmoothly(1, shootPoint, _settings.RayAppearanceDuration, () =>
            {
                deActivatable?.DeActivate();
                _timer.Pinpoint();

                _hitEffect.transform.position = shootPoint;
                _hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, targetHit.normal);
                _graphicsRay.SetPositionSmoothly(0, _graphicsRay.GetPosition(1), _settings.RayAppearanceDuration);
                transform.DORotate(_defaultRotation, _settings.RotateDuration);
            });
        });

        return true;
    }

    private void Start()
    {
        _timer = GetComponent<CTimer>();
        _timer.OnCompletion.AddListener(() => _canShoot = true);

        _graphicsRay.positionCount = 2;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)) && _mouseRaycastInput.Get(out RaycastHit hit)) TryShoot(hit);
    }
}
