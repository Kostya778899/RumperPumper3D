using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionInWorldByRaycastInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;


    public bool Get(out RaycastHit hit) => Get(out hit, out Ray ray);
    public bool Get(out RaycastHit hit, out Ray ray)
    {
        ray = _camera.ScreenPointToRay(Input.mousePosition);

        VisualizeRay(ray, Color.green);
        return Physics.Raycast(ray, out hit);
    }
    private void VisualizeRay(in Ray ray, in Color color) => Debug.DrawRay(_camera.transform.position, ray.direction * 100f, color);
}
