using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    [SerializeField] private DetectSwipeMethods _detectSwipeMethod = DetectSwipeMethods.OnEnd;
    [SerializeField] private float _threshold = 20f;

    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;

    private enum DetectSwipeMethods { OnStart, OnEnd }


    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerUpPosition = touch.position;
                _fingerDownPosition = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (_detectSwipeMethod == DetectSwipeMethods.OnEnd)
                {
                    _fingerDownPosition = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                _fingerDownPosition = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > _threshold && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (_fingerDownPosition.y - _fingerUpPosition.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (_fingerDownPosition.y - _fingerUpPosition.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            _fingerUpPosition = _fingerDownPosition;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > _threshold && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (_fingerDownPosition.x - _fingerUpPosition.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (_fingerDownPosition.x - _fingerUpPosition.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            _fingerUpPosition = _fingerDownPosition;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
    }
}
