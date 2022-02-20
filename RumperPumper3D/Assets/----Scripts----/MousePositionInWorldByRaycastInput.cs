using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionInWorldByRaycastInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;


    public RaycastHit? Get(out Ray ray)
    {
        ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            VisualizeRay(ray, Color.green);
            return hit;
        }
        VisualizeRay(ray, Color.gray);
        return null;
    }
    private void VisualizeRay(in Ray ray, in Color color) => Debug.DrawRay(_camera.transform.position, ray.direction * 100f, color);
}
