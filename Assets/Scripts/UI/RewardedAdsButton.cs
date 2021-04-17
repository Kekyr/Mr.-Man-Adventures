using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    public static bool isRewardedAdOn;

    public GameObject rewardedAd;
    public Button rewardedAdButton;
    public AudioClip healSFX;
    private AudioManager audioManager;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;


    public string mySurfacingId = "Rewarded_Android";

    private void Start()
    {
        audioManager = AudioManager.instance;
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        rewardedAdButton = GetComponent<Button>();

        rewardedAdButton.interactable = Advertisement.IsReady(mySurfacingId);

        if (rewardedAdButton)
        {
            rewardedAdButton.onClick.AddListener(ShowRewardedVideo);
        }

        Advertisement.AddListener(this);

    }


    void ShowRewardedVideo()
    {
        Advertisement.Show(mySurfacingId);
    }


    public void OnUnityAdsReady(string surfacingId)
    {
        if (surfacingId == mySurfacingId)
        {
            rewardedAdButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            audioManager.PlaySFX(healSFX);
            playerHealth.healthPoints += 1;
            playerHealth.hearts[playerHealth.healthPoints - 1].ChangeSprite();
            Time.timeScale = 1f;
            playerMovement.enabled = true;
            PauseMenu.gameIsPaused = false;
            isRewardedAdOn = false;
            rewardedAd.SetActive(false);
        }
    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string surfacingId)
    {

    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    public void RewardedAdExitButton()
    {
        Time.timeScale = 1f;
        playerMovement.enabled = true;
        PauseMenu.gameIsPaused = false;
        isRewardedAdOn = false;
        rewardedAd.SetActive(false);
    }
}
