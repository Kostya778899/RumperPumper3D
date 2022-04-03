using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using CMath;
using UnityEngine.Events;

public class ContinueGameSessionButton : MonoBehaviour, IActivatable
{
    [SerializeField] private GoogleAdmobRewardedAd _rewardedAd;
    [SerializeField] private UnityEvent _continue;


    public void Activate()
    {
        _rewardedAd.TryShow();

        _rewardedAd.Ad.OnAdClosed += OnAdClosed;
        _rewardedAd.Ad.OnAdFailedToLoad += OnAdFailedToLoad;

        void OnAdClosed<TEventArgs>(object sender, TEventArgs e)
        {
            OnAdWorked();
            _continue?.Invoke();
        }
        void OnAdFailedToLoad<TEventArgs>(object sender, TEventArgs e)
        {
            OnAdWorked();
        }
        void OnAdWorked()
        {
            _rewardedAd.Ad.OnAdClosed -= OnAdClosed;
            _rewardedAd.Ad.OnAdFailedToLoad -= OnAdFailedToLoad;
        }
    }
}
