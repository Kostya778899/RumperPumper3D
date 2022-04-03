using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Google Admob/Ads/Averange", fileName = "NewAverangeAd")]
public class GoogleAdmobAverangeAd : GoogleAdmobAd
{
    private InterstitialAd _ad;
    public InterstitialAd Ad => _ad;


    protected override void Request()
    {
        _ad = new InterstitialAd(GetId());
        AdRequest request = new AdRequest.Builder().Build();
        _ad.LoadAd(request);
    }

    public override bool TryShow()
    {
        Request();
        _ad.Show();
        return _ad.IsLoaded();
    }
    public override void TryShow_() => TryShow();
}
