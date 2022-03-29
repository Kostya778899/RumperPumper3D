using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerPositionsToMove _positionsToMove;

    [Min(0f), SerializeField] private float _moveDuration = 0.4f;
    [Min(0f), SerializeField] private float _moveRotateDuration = 0.2f;
    [SerializeField] private CMath.Vector3Bool _moveEnabledAxis = CMath.Vector3Bool.True;
    [SerializeField] private float _rotateAngleToMovePosition = 15f;
    [SerializeField] private Vector3 _defaultRotation = Vector3.zero;
    [SerializeField] private Ease _moveEase = Ease.InOutSine;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private AnimationCurve _jumpCurve;

    private Rigidbody _rigidbody;
    private int _currentPositionIndex = 0;
    private bool _isJumping = false;


    public void TryMove(int direction)
    {
        int newPositionIndex = _currentPositionIndex + direction;
        if (newPositionIndex >= 0 && newPositionIndex < _positionsToMove.GetPositions().Length)
        {
            _currentPositionIndex = newPositionIndex;
            Move(_currentPositionIndex, direction);
        }
    }
    public void TryJump()
    {
        if (!_isJumping)
        {
            _isJumping = true;
            Jump(() => _isJumping = false);
        }
    }

    private void Move(int newPositionIndex, int direction)
    {
        Vector3 newPosition = _positionsToMove.GetPositions()[newPositionIndex].position;

        if (_moveEnabledAxis.x) transform.DOMoveX(newPosition.x, _moveDuration).SetEase(_moveEase);
        if (_moveEnabledAxis.y) transform.DOMoveY(newPosition.y, _moveDuration).SetEase(_moveEase);
        if (_moveEnabledAxis.z) transform.DOMoveZ(newPosition.z, _moveDuration).SetEase(_moveEase);

        transform.DORotate(_defaultRotation +
            new Vector3(0f, direction * _rotateAngleToMovePosition, 0f), _moveRotateDuration).SetEase(_moveEase)
            .OnComplete(() =>
            transform.DORotate(_defaultRotation, _moveRotateDuration).SetEase(_moveEase));
    }
    private void Jump(Action callback) => transform.DOMoveY(_jumpHeight, _jumpDuration).SetEase(_jumpCurve).OnComplete(() => callback?.Invoke());

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Move(0, 0);
    }
}
