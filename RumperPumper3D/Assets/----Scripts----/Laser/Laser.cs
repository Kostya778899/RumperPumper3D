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
    //[SerializeField] private LineRenderer _graphicsRay;
    [SerializeField] private Transform _hitEffect;

    [SerializeField] private Vector3 _defaultRotation;

    private Coroutine _shoot = null;
    private bool _isShooting = false;


    public void TryShoot()
    {
        RaycastHit hit = new RaycastHit();
        IDeActivatable deActivatable = null;
        if (_isShooting || !_mouseRaycastInput.Get(out hit) || !hit.collider.TryGetComponent(out deActivatable)) return;
        _isShooting = true;

        Vector3 shootPoint = hit.transform.position;
        Vector3 newRotation = Quaternion.LookRotation(transform.position - shootPoint).eulerAngles + _defaultRotation;

        transform.DORotate(newRotation, _settings.RotateDuration).OnComplete(() =>
        {
            var graphicsRay = Instantiate<LineRenderer>(_settings.GraphicsRayPrefab);
            graphicsRay.positionCount = 2;
            graphicsRay.SetPositions(transform.position);

            graphicsRay.SetPositionSmoothly(1, shootPoint, _settings.RayAppearanceDuration, () =>
            {
                deActivatable?.DeActivate();

                //_hitEffect.transform.position = shootPoint;
                //_hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, targetHit.normal);
                graphicsRay.SetPositionSmoothly(0, graphicsRay.GetPosition(1), _settings.RayAppearanceDuration, () =>
                {
                    _isShooting = false;
                    Destroy(graphicsRay);
                });
                //transform.DORotate(_defaultRotation, _settings.RotateDuration).OnComplete(() => _isShooting = false);
            });
        });
        //_isShooting = false;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began))
    //    {
    //        TryShoot();
    //    }
    //}
}
