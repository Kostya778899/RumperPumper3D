using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerPositionsToMove _positionsToMove;

    [Min(0f), SerializeField] private float _moveDuration = 0.4f;
    [Min(0f), SerializeField] private float _moveRotateDuration = 0.2f;
    [SerializeField] private float _rotateAngleToMovePosition = 15f;
    [SerializeField] private Vector3 _defaultRotation = Vector3.zero;
    [SerializeField] private Ease _moveEase = Ease.InOutSine;

    private int _currentPositionIndex = 0;


    public void TryMove(int direction)
    {
        int newPositionIndex = _currentPositionIndex + direction;
        if (newPositionIndex >= 0 && newPositionIndex < _positionsToMove.GetPositions().Length)
        {
            _currentPositionIndex = newPositionIndex;
            Move(_currentPositionIndex, direction);
        }
    }
    private void Move(int newPositionIndex, int direction)
    {
        Vector3 newPosition = _positionsToMove.GetPositions()[newPositionIndex].position;

        transform.DOMove(newPosition, _moveDuration).SetEase(_moveEase);
        transform.DORotate(_defaultRotation +
            new Vector3(0f, direction * _rotateAngleToMovePosition, 0f), _moveRotateDuration).SetEase(_moveEase)
            .OnComplete(() =>
            transform.DORotate(_defaultRotation, _moveRotateDuration).SetEase(_moveEase));
    }

    private void Start()
    {
        Move(0, 0);
    }
}
