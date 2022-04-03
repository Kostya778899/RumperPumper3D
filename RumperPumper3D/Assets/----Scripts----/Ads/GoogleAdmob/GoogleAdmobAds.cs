using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdmobAds : MonoBehaviour
{
    [SerializeField] private GoogleAdmobAd[] _ads;


    private void Awake() => MobileAds.Initialize(initStatus => { });

    private void Start()
    {
        for (int i = 0; i < _ads.Length; i++)
        {
            _ads[i].TryShow();
        }
    }
}
