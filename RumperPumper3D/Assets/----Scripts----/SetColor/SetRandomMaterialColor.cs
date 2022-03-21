using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;
using UnityEngine.Events;

public class SetRandomMaterialColor : SetMaterialColor, IUpdatable
{
    [SerializeField] private Gradient _colors;
    [SerializeField] private bool _updatingOnAwake = false;
    [SerializeField] private UnityEvent<Color> _onUpdatingColor;


    public void Updating()
    {
        Color newColor = _colors.Evaluate(UnityEngine.Random.value);
        MeshRenderer.material.color = newColor;
        _onUpdatingColor?.Invoke(newColor);
    }

    private void Awake() { if (_updatingOnAwake) Updating(); }
}
