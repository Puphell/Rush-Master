using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI; // Make sure to include this for the Button component

public class ShopManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public PlayerController playerController;
    public int speedUpgradeCost = 100; // Hýz arttýrma maliyeti
    private int currentCoins;
    public GameObject[] unlockables; // Unlock edilecek objeleri buraya ekle
    public Color[] colors; // Renkleri temsil eden dizi

    private string _androidGameId = "5671035";
    private string _interstitialAdUnitId = "Interstitial_Android";
    private string _rewardedAdUnitId = "Rewarded_Android";
    private bool _isInterstitialLoaded;
    private bool _isRewardedAdLoaded;

    private string rewardType; // rewardType deðiþkeni tanýmlandý

    private void Start()
    {
        // Unity Ads'i baþlat
        Advertisement.Initialize(_androidGameId, true, this);

        // Oyuncunun hýzýný yükle
        playerController.moveSpeed = PlayerPrefs.GetFloat("PlayerSpeed", playerController.moveSpeed);
        // Kaydedilen coin sayýsýný yükle
        currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        // Baþlangýçta açýlmýþ olan butonlarý kontrol edip unlock yap
        for (int i = 0; i < unlockables.Length; i++)
        {
            Button unlockableButton = unlockables[i].GetComponent<Button>();
            if (PlayerPrefs.GetInt("Unlockable_" + i, 0) == 1)
            {
                unlockables[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                if (unlockableButton != null)
                {
                    unlockableButton.interactable = false;
                }
            }
        }

        LoadInterstitialAd();
        LoadRewardedAd();
    }

    void Update()
    {
        currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    public void SubtractCoins(int amount)
    {
        currentCoins -= amount;
        PlayerPrefs.SetInt("TotalCoins", currentCoins);
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    public void UnlockRandom()
    {
        int randomIndex = -1;
        for (int i = 0; i < unlockables.Length; i++)
        {
            int index = Random.Range(0, unlockables.Length);
            if (PlayerPrefs.GetInt("Unlockable_" + index, 0) == 0)
            {
                randomIndex = index;
                break;
            }
        }

        if (randomIndex != -1)
        {
            unlockables[randomIndex].transform.GetChild(1).gameObject.SetActive(false);
            Button unlockableButton = unlockables[randomIndex].GetComponent<Button>();
            if (unlockableButton != null)
            {
                unlockableButton.interactable = true;
            }

            // Rengi kaydet ve açýlmýþ olarak iþaretle
            PlayerPrefs.SetInt("UnlockedColorIndex", randomIndex);
            PlayerPrefs.SetInt("Unlockable_" + randomIndex, 1);
        }
        else
        {
            Debug.Log("Tüm renkler zaten açýldý!");
        }
    }

    public void UnlockRandomCoin()
    {
        if (currentCoins >= 1000)
        {
            SubtractCoins(1000);

            int randomIndex = -1;
            for (int i = 0; i < unlockables.Length; i++)
            {
                int index = Random.Range(0, unlockables.Length);
                if (PlayerPrefs.GetInt("Unlockable_" + index, 0) == 0)
                {
                    randomIndex = index;
                    break;
                }
            }

            if (randomIndex != -1)
            {
                unlockables[randomIndex].transform.GetChild(1).gameObject.SetActive(false);
                Button unlockableButton = unlockables[randomIndex].GetComponent<Button>();
                if (unlockableButton != null)
                {
                    unlockableButton.interactable = true;
                }

                // Rengi kaydet ve açýlmýþ olarak iþaretle
                PlayerPrefs.SetInt("UnlockedColorIndex", randomIndex);
                PlayerPrefs.SetInt("Unlockable_" + randomIndex, 1);
            }
            else
            {
                Debug.Log("Tüm renkler zaten açýldý!");
            }
        }
        else
        {
            Debug.Log("Yeterli coin yok!");
        }
    }

    public void SpeedUpgrade()
    {
        playerController.moveSpeed += 1f;
        PlayerPrefs.SetFloat("PlayerSpeed", playerController.moveSpeed);
    }

    public void SpeedUpgradeVideoAd()
    {
        rewardType = "SpeedUpgrade"; // rewardType'ý ayarla
        ShowRewardedAd();
    }

    public void RewardedAdCoin()
    {
        rewardType = "500Coin";
        ShowRewardedAd();
    }

    public void SpeedUpgradeCoin()
    {
        if (currentCoins >= speedUpgradeCost)
        {
            SubtractCoins(speedUpgradeCost);
            playerController.moveSpeed += 1f;
            PlayerPrefs.SetFloat("PlayerSpeed", playerController.moveSpeed);
        }
        else
        {
            Debug.Log("Yeterli coin yok!");
        }
    }

    public void UnlockRandomVideoAd()
    {
        rewardType = "UnlockRandom"; // rewardType'ý ayarla
        ShowRewardedAd();
    }

    private void ShowRewardedAd()
    {
        if (_isRewardedAdLoaded)
        {
            Advertisement.Show(_rewardedAdUnitId, this);
        }
        else
        {
            Debug.Log("Rewarded ad not loaded. Attempting to load again.");
            LoadRewardedAd();
        }
    }

    void LoadInterstitialAd()
    {
        Advertisement.Load(_interstitialAdUnitId, this);
    }

    private void LoadRewardedAd()
    {
        Advertisement.Load(_rewardedAdUnitId, this);
    }

    public void ShowInterstitial()
    {
        if (_isInterstitialLoaded)
        {
            Advertisement.Show(_interstitialAdUnitId, this);
        }
        else
        {
            Debug.Log("Interstitial ad not loaded.");
            LoadInterstitialAd();
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        Advertisement.Load(_rewardedAdUnitId, this);

        Debug.Log("Unity Ads initialization complete.");
        LoadInterstitialAd();
        Advertisement.Load(_rewardedAdUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(_interstitialAdUnitId))
        {
            _isInterstitialLoaded = true;
        }
        else if (adUnitId.Equals(_rewardedAdUnitId))
        {
            _isRewardedAdLoaded = true;
            Debug.Log("Rewarded Ad Loaded Successfully.");
        }
    }
    

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Reklam yüklenemezse belirli bir süre sonra tekrar denemek için bir geri arama veya zamanlayýcý ekleyebilirsiniz.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Reklam gösterilemezse belirli bir süre sonra tekrar denemek için bir geri arama veya zamanlayýcý ekleyebilirsiniz.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_rewardedAdUnitId) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            OnRewardedAdComplete();
            LoadRewardedAd();
        }
    }

    public void OnRewardedAdComplete()
    {
        if (rewardType == "SpeedUpgrade")
        {
            SpeedUpgrade();
        }
        else if (rewardType == "UnlockRandom")
        {
            UnlockRandom();
        }
        else if (rewardType == "500Coin")
        {
            CoinManager coinManager = FindObjectOfType<CoinManager>();

            if (coinManager != null)
            {
                coinManager.AddCoins(500);

                Debug.Log("500 Coin eklendi");
            }
            else
            {
                Debug.LogError("CoinManager bulunamadý! CoinManager script'i sahnede mevcut mu?");
            }
        }
    }
}