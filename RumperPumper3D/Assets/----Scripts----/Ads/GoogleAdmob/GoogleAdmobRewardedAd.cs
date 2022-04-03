using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

[CreateAssetMenu(menuName = "Google Admob/Ads/Rewarded", fileName = "NewRewardedAd")]
public class GoogleAdmobRewardedAd : GoogleAdmobAd, IRewardedAd
{
    [HideInInspector] public RewardedAd Ad { get; private set; }


    protected override void Request()
    {
        Ad = new RewardedAd(GetId());

        Ad.OnAdLoaded += OnLoaded;
        Ad.OnAdFailedToLoad += OnLoadedError;
        Ad.OnUserEarnedReward += OnViewed;
        Ad.OnAdClosed += OnClosed;
        Ad.OnAdOpening += OnOpeningAnnouncement;

        AdRequest request = new AdRequest.Builder().Build();
        Ad.LoadAd(request);
    }

    public override bool TryShow()
    {
        Request();
        Ad.Show();
        return Ad.IsLoaded();
    }
    public override void TryShow_() => TryShow();

    public void OnLoaded(object sender, EventArgs eventArgs)/* => Debug.Log("");*/{ }
    public void OnLoadedError(object sender, AdFailedToLoadEventArgs eventArgs) => Debug.Log("");
    public void OnViewed(object sender, EventArgs eventArgs)/* => Debug.Log("");*/{ }
    public void OnClosed(object sender, EventArgs eventArgs)/* => Debug.Log("");*/{ }
    public void OnOpeningAnnouncement(object sender, EventArgs eventArgs) => Debug.Log("");
}
