using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using CMath;

public class CutScene : MonoBehaviour, IIncluded
{
    public UnityEvent OnActivate, OnDeActivate;


    public void Activate()
    {


        OnActivate?.Invoke();
    }
    public void DeActivate()
    {


        OnDeActivate?.Invoke();
    }
}
