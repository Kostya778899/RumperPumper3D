using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Laser : MonoBehaviour
{
    [SerializeField] private LaserSettings _settings;
    [SerializeField] private MousePositionInWorldByRaycastInput _mouseRaycastInput;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LineRenderer _graphicsRay;
    [SerializeField] private Transform _hitEffect;

    [SerializeField] private Vector3 _defaultRotation;


    public void Shoot(RaycastHit targetHit)
    {
        //transform.rotation = Quaternion.Euler(Quaternion.LookRotation(transform.position - target).eulerAngles + _defaultRotation);
        //return;

        Debug.Log(Quaternion.LookRotation(transform.position - targetHit.transform.position, transform.rotation.eulerAngles).eulerAngles);

        DOTween.To(() => transform.rotation.eulerAngles, x =>
        transform.rotation = Quaternion.Euler(x), (Quaternion.LookRotation(transform.position - targetHit.transform.position).eulerAngles + _defaultRotation).Place(360f), 1).OnComplete(() =>
        {
            _graphicsRay.SetPositions(_shootPoint.position);

            _graphicsRay.SetPositionSmoothly(1, targetHit.transform.position, _settings.RayAppearanceDuration, () =>
            {
                _hitEffect.transform.position = targetHit.transform.position;
                _hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, targetHit.normal);

                targetHit.collider.gameObject.GetComponent<DestroyableByLaser>()?.Destroy(targetHit);

                _graphicsRay.SetPositionSmoothly(0, _graphicsRay.GetPosition(1), _settings.RayAppearanceDuration);
            });
        });
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
                if (hit.collider.GetComponent<DestroyableByLaser>())
                {
                    Shoot(hit);
                }
            }
        }
    }
}
