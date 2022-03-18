using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CMath;

public class Laser : MonoBehaviour
{
    [SerializeField] private LaserSettings _settings;
    [SerializeField] private MousePositionInWorldByRaycastInput _mouseRaycastInput;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LineRenderer _graphicsRay;
    [SerializeField] private Transform _hitEffect;

    [SerializeField] private Vector3 _defaultRotation;

    private Coroutine _shoot = null;
    private bool _isShooting = false;


    public void TryShoot()
    {
        if (_isShooting) return;

        RaycastHit hit = new RaycastHit();
        IDeActivatableByLaser deActivatable = null;
        if (!(_mouseRaycastInput.Get(out hit) && hit.collider.TryGetComponent(out deActivatable))) return;
        _isShooting = true;

        Vector3 shootPoint = hit.transform.position;
        Vector3 newRotation = Quaternion.LookRotation(transform.position - shootPoint).eulerAngles + _defaultRotation;

        transform.DORotate(newRotation, _settings.RotateDuration).OnComplete(() =>
        {
            _graphicsRay.SetPositions(_shootPoint.position);

            _graphicsRay.SetPositionSmoothly(1, shootPoint, _settings.RayAppearanceDuration, () =>
            {
                StartCoroutine(OnVisuallyHit());
                IEnumerator OnVisuallyHit()
                {
                    deActivatable?.DeActivate();
                    yield return new WaitForSeconds(_settings.RayDuration);
                    _graphicsRay.SetPositionSmoothly(0, _graphicsRay.GetPosition(1), _settings.RayAppearanceDuration, () => _isShooting = false);
                }
            });
        });
    }

    private void Start() => _graphicsRay.positionCount = 2;
}
