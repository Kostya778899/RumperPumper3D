using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialColor : MonoBehaviour, ISetColor
{
    [SerializeField] protected MeshRenderer MeshRenderer;


    public void SetColor(Color color) => MeshRenderer.material.color = color;
}
