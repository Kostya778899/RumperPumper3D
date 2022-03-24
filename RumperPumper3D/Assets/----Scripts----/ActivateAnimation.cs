using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMath;

public class ActivateAnimation : MonoBehaviour, IActivatable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _animation;
    [SerializeField] private bool _activateOnAwake = false;


    public void Activate() => _animator.Play(_animation.name.Replace('.', '_'));

    private void Awake() { if (_activateOnAwake) Activate(); }
}
