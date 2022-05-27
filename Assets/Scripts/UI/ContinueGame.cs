using UnityEngine;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
    [SerializeField] private SideController side;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private LastStep lastStep;

    private void Start()
    {
        RewardedAdsButton.AdsSingleton.adCompleted += SetContinue;
        RewardedAdsButton.AdsSingleton.adLoaded += SetInteractable;
    }

    public void TryShowAd()
    {
        RewardedAdsButton.AdsSingleton?.ShowAd();
    }

    public void SetInteractable(bool isInteractable)
    {
        GetComponent<Button>().interactable = isInteractable;
    }

    public void SetContinue()
    {
        Invoke("DeleteSectionAndContinue", 0.1f);
    }

    public void DeleteSectionAndContinue()
    {
        GameOver.isGameOver = false;
        PlayerPrefs.SetInt("isGameOver", 0);
        lastStep.StopShowDangerous();
        side.ContinueGame();
        HideMenu();
    }

    public void HideMenu()
    {
        Brick.isGame = true;
        gameOverCanvas.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        RewardedAdsButton.AdsSingleton.adCompleted -= SetContinue;
        RewardedAdsButton.AdsSingleton.adLoaded -= SetInteractable;
    }
}
