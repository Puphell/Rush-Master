using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    public GameObject winPanel;
    public GameObject gameOverPanel;
    public List<MonoBehaviour> disableScripts;

    public Text coinText;

    public Button restartButton;

    private int totalCoins;

    void Start()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinText();

        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void UpdateCoinText()
    {
        coinText.text = " " + totalCoins;
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (var script in disableScripts)
        {
            script.enabled = false;
        }

        if (other.CompareTag("Player"))
        {
            winPanel.SetActive(true);

            playerAnimator.SetTrigger("Win");
            enemyAnimator.SetTrigger("Cry");

            EnemyController enemy = FindObjectOfType<EnemyController>();
            if (enemy != null)
            {
                enemy.moveSpeed += 1f;
                PlayerPrefs.SetFloat("EnemySpeed", enemy.moveSpeed);
            }

            CoinManager coinManager = FindObjectOfType<CoinManager>();
            if (coinManager != null)
            {
                coinManager.AddCoins(50); // Use AddCoins to add 50 coins
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            gameOverPanel.SetActive(true);

            enemyAnimator.SetTrigger("Win");
            playerAnimator.SetTrigger("Cry");

            restartButton.interactable = false;

            Invoke("ShowAdInvoke", 1f);
        }
    }

    void ShowAdInvoke()
    {
        ShopManager shopManager = FindObjectOfType<ShopManager>();
        if (shopManager != null)
        {
            shopManager.ShowInterstitial();
        }

        restartButton.interactable = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}