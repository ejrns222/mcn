using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class CAdManager : MonoBehaviour
{
    //private RewardBasedVideoAd _ad;
    private RewardedAd _ad;
    private string _addId = "ca-app-pub-8112594412775997~6378571550";
    private string _unitId = "ca-app-pub-8112594412775997/9076779283";
    private string _testUintId = "ca-app-pub-3940256099942544/5224354917";

    public delegate void OnRewardEnd();

    public OnRewardEnd rewardHandler; 
    

    private void Start()
    {
        /*MobileAds.Initialize(_addId);
        _ad = RewardBasedVideoAd.Instance;

        _ad.OnAdRewarded += OnAdRewaded;*/
        _ad = new RewardedAd(_unitId);
        
        _ad.OnAdClosed += HandleRewardedAdClosed;
        _ad.OnUserEarnedReward += OnAdRewarded;
        
        LoadAd();
    }

    private void OnAdRewarded(object sender, Reward e)
    {
        rewardHandler.Invoke();
        Debug.Log("리워드");
    }
    
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        LoadAd();
        Debug.Log("클로즈");
    }

    private void LoadAd()
    {
        /*AdRequest request = new AdRequest.Builder().Build();
        _ad.LoadAd(request,_unitId);*/
        AdRequest request = new AdRequest.Builder().Build();
        _ad.LoadAd(request);
        
    }

    public void ShowAD()
    {
        if (_ad.IsLoaded())
        {
            Debug.Log("광고나온다");
            
            _ad.Show();
        }
        else
        {
            Debug.Log("광고안나온다");
            LoadAd();
        }
    }
        
}
