using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static RewardedAdsButton AdsSingleton;

    [SerializeField] private Button button;
    [SerializeField] private string androidAdId = "Rewarded_Android";
    [SerializeField] private string iOSAdId = "Rewarded_iOS";
    private string adId = null;
    public event Action adCompleted;
    public event Action<bool> adLoaded;

    private void Awake()
    {
        if (AdsSingleton == null)
        {
            AdsSingleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

#if UNITY_IOS
        adId = iOSAdId;
#elif UNITY_ANDROID
        adId = androidAdId;
#endif
    }
    
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + adId);
        Advertisement.Load(adId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        adLoaded?.Invoke(adUnitId.Equals(adId));
    }

    public void ShowAd()
    {
        adLoaded?.Invoke(false);
        Advertisement.Show(adId, this);
    }
    
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(adId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            Advertisement.Load(adId, this);

            adCompleted?.Invoke();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }

    public void OnUnityAdsShowClick(string adUnitId) { }
}
