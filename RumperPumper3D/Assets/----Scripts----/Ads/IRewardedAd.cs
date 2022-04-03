using System;
using GoogleMobileAds.Api;

interface IRewardedAd
{
    void OnLoaded(object sender, EventArgs eventArgs);
    void OnLoadedError(object sender, AdFailedToLoadEventArgs eventArgs);
    void OnViewed(object sender, EventArgs eventArgs);
    void OnClosed(object sender, EventArgs eventArgs);
    void OnOpeningAnnouncement(object sender, EventArgs eventArgs);
}
