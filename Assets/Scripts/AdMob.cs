using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdMob : MonoBehaviour
{
    enum AdStatus
    {
        Empty,
        Waiting,
        Failed,
        Success
    }


    BannerView bannerView;
    InterstitialAd interstitialAd;

    float bannerTimer = 0f;
    float interstitialTimer = 0f;
    int deathCount = 0;

    [SerializeField]
    private string _bannerUnitId = "ca-app-pub-3940256099942544/6300978111";

    [SerializeField]
    private string _interstitialUnitId = "ca-app-pub-3940256099942544/8691691433";

    AdStatus bannerAdStatus = AdStatus.Empty;
    AdStatus interstitialAdStatus = AdStatus.Empty;

    private void Awake()
    {
        if (FindObjectsOfType<AdMob>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        RequestInterstitial();

        PlayerController.onDie += IncreaseDeathCount;
    }


    private void OnDisable()
    {
        PlayerController.onDie -= IncreaseDeathCount;
    }

    private void Update()
    {
        if (bannerAdStatus == AdStatus.Failed)
        {
            bannerTimer += Time.deltaTime;

            if (bannerTimer > 10f)
            {                
                RequestBanner();
                bannerTimer = 0f;
            }
        }

        if (interstitialAdStatus == AdStatus.Failed)
        {
            interstitialTimer += Time.deltaTime;

            if (interstitialTimer > 10f)
            {                
                RequestInterstitial();
                interstitialTimer = 0f;
            }
        }
    }


    private void RequestInterstitial()
    {
        if (interstitialAdStatus == AdStatus.Empty)
        {
            interstitialAd = new InterstitialAd(_interstitialUnitId);

            interstitialAd.OnAdLoaded += HandleOnInterstitialAdLoaded;
            interstitialAd.OnAdFailedToLoad += HandleOnInterstitialAdFailedToLoad;
            interstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;

        }

        AdRequest adRequest = new AdRequest.Builder().Build();

        interstitialAd.LoadAd(adRequest);
    }
    void RequestBanner()
    {
        if (bannerAdStatus == AdStatus.Empty)
        {
            bannerView = new BannerView(_bannerUnitId, AdSize.Banner, AdPosition.Bottom);

            bannerView.OnAdLoaded += HandleOnBannerAdLoaded;
            bannerView.OnAdFailedToLoad += HandleOnBannerAdFailedToLoad;
        }

        AdRequest adRequest = new AdRequest.Builder().Build();

        bannerView.LoadAd(adRequest);
       //adStatus = AdStatus.Waiting;
    }

    public void HandleOnBannerAdLoaded(object sender, EventArgs args)
    {
        bannerAdStatus = AdStatus.Success;
    }

    public void HandleOnBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        bannerAdStatus = AdStatus.Failed;
    }

    void IncreaseDeathCount()
    {
        deathCount++;

        if (deathCount >= 5)
        {
            if (interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
                deathCount = 0;
            }

        }
    }

    public void HandleOnInterstitialAdLoaded(object sender, EventArgs args)
    {
        interstitialAdStatus = AdStatus.Success;
    }

    public void HandleOnInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        interstitialAdStatus = AdStatus.Failed;
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }

}
