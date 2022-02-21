using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Laser : MonoBehaviour
{
    [SerializeField] private MousePositionInWorldByRaycastInput _mouseRaycastInput;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LineRenderer _graphicsRay;
    [SerializeField] private Transform _hitEffect;

    [Min(0f), SerializeField] private float _lineDistance = 100f;


    public void Shoot(in Vector3 point)
    {
        transform.DODynamicLookAt(point, 0.25f).OnComplete(() =>
        {
            _graphicsRay.SetPosition(0, _shootPoint.position);

            if (Physics.Raycast(transform.position, transform.forward * _lineDistance, out RaycastHit hit))
            {
                InitializeGraphicsRay(_shootPoint.position, hit.collider.gameObject.transform.position, () =>
                {
                    _hitEffect.transform.position = hit.point;
                    _hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                    hit.collider.gameObject.GetComponent<DestroyableByLaser>()?.Destroy(hit);
                });
            }
            else
            {
                InitializeGraphicsRay(_shootPoint.position, transform.forward * _lineDistance);
            }
        });

        void InitializeGraphicsRay(in Vector3 startPoint, in Vector3 endPoint, Action callback = null)
        {
            for (int i = 0; i < _graphicsRay.positionCount; i++) _graphicsRay.SetPosition(1, startPoint);
            DOTween.To(() => _graphicsRay.GetPosition(0), x => _graphicsRay.SetPosition(1, x), endPoint,
                Vector3.Distance(_graphicsRay.GetPosition(0), endPoint) * 0.05f).OnComplete(() => callback?.Invoke());
        }
    }

    private void Start()
    {
        _graphicsRay.positionCount = 2;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began))
        {
            if (_mouseRaycastInput.Get(out RaycastHit hit))
            {
                Shoot(hit.collider.gameObject.transform.position);
            }
        }
    }
}
