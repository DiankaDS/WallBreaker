using UnityEngine;
using UnityEngine.Advertisements;
 
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    private string androidGameId = "4770499";
    private string iOSGameId = "4770498";
    private bool testMode = true;
    private string gameId;
 
    private void Awake()
    {
#if UNITY_EDITOR
  Debug.logger.logEnabled = true;
#else
  Debug.logger.logEnabled = false;
#endif
        InitializeAds();
    }

    public void InitializeAds()
    {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSGameId
            : androidGameId;
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        GetComponent<RewardedAdsButton>().LoadAd();
    }
    
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
