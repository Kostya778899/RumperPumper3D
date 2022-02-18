using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeInput : MonoBehaviour
{
    [SerializeField] private DetectSwipeMethods _detectSwipeMethod = DetectSwipeMethods.OnEnd;
    [SerializeField] private float _threshold = 20f;

    [SerializeField] private UnityEvent<Vector2> _onSwipe;
    [SerializeField] private UnityEvent _onSwipeUp, _onSwipeDown, _onSwipeLeft, _onSwipeRight;

    private Vector2 _fingerStartPosition;
    private Vector2 _fingerEndPosition;
    private Vector2 _swipeDirection;

    private enum DetectSwipeMethods { OnUpdate, OnEnd }


    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerEndPosition = touch.position;
                _fingerStartPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (_detectSwipeMethod == DetectSwipeMethods.OnUpdate)
                {
                    _fingerStartPosition = touch.position;
                    CheckSwipe();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _fingerStartPosition = touch.position;
                CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        if (Vector2.Distance(_fingerStartPosition, _fingerEndPosition) >= _threshold)
        {
            _swipeDirection = _fingerStartPosition - _fingerEndPosition;
            _onSwipe?.Invoke(_swipeDirection);
            if (Math.Abs(_swipeDirection.x) > Math.Abs(_swipeDirection.y))
                if (_swipeDirection.x > 0f) _onSwipeRight?.Invoke(); else _onSwipeLeft?.Invoke();
            else
                if (_swipeDirection.y > 0f) _onSwipeUp?.Invoke(); else _onSwipeDown?.Invoke();
        }
    }
}
