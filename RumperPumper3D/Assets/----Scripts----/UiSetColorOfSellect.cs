using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using CMath;

public class UiSetColorOfSellect : MonoBehaviour, IIncluded
{
    [SerializeField] private Color _sellectedColor = Color.green, _deSellectedColor = Color.red;
    [SerializeField] private Image _image;


    public void Activate() => _image.color = _sellectedColor;
    public void DeActivate() => _image.color = _deSellectedColor;
}
