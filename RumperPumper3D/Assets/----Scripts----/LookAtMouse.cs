using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _estrangementByCamera = 0.5f;

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        float estrangementPoint = (transform.position - _camera.transform.position).magnitude * _estrangementByCamera;
        transform.LookAt(ray.origin + ray.direction * estrangementPoint);
    }
}
