using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text coinText;

    private int totalCoins;

    void Start()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinText();
    }

    void Update()
    {
        // Remove this if you only need to update the text when coins are added/removed
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinText();
    }

    public void AddCoin()
    {
        totalCoins++;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        UpdateCoinText();
    }

    public void SubtractCoins(int amount)
    {
        totalCoins -= amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        UpdateCoinText();
    }

    public int GetCurrentCoins()
    {
        return totalCoins;
    }

    private void UpdateCoinText()
    {
        coinText.text = " " + totalCoins;
    }
}