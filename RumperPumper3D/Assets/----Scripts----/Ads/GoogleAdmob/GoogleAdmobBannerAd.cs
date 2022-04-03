using GoogleMobileAds.Api;
using System;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Google Admob/Ads/Banner", fileName = "NewBannerAd")]
public class GoogleAdmobBannerAd : GoogleAdmobAd
{
    [SerializeField] private AdPosition _position;

    private BannerView _ad;


    protected override void Request()
    {
        _ad = new BannerView(GetId(), AdSize.Banner, _position);
        AdRequest request = new AdRequest.Builder().Build();
        _ad.LoadAd(request);
    }

    public override bool TryShow()
    {
        Request();
        _ad.Show();
        return true;
    }
    public override void TryShow_() => TryShow();
}
