using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    public GameObject startPanel;
    public GameObject shopPanel;
    public GameObject creditPanel;

    public Text playerSpeedText;
    public Text enemySpeedText;

    // List to hold scripts that need to be enabled/disabled
    public List<MonoBehaviour> disableScripts;

    void Start()
    {
        // Disable all scripts in the disableScripts list
        foreach (var script in disableScripts)
        {
            script.enabled = false;
        }

        startPanel.SetActive(true);
    }

    void Update()
    {
        UpdateSpeedText();
    }

    public void TriggerClick()
    {
        startPanel.SetActive(false);

        // Enable all scripts in the disableScripts list
        foreach (var script in disableScripts)
        {
            script.enabled = true;
        }

        playerAnimator.SetTrigger("Run");
        enemyAnimator.SetTrigger("Run");
    }

    public void shopPanelOpen()
    {
        shopPanel.SetActive(true);
    }

    public void shopPanelExit()
    {
        shopPanel.SetActive(false);
    }

    public void creditPanelOpen()
    {
        creditPanel.SetActive(true);
    }

    public void creditPanelExit()
    {
        creditPanel.SetActive(false);
    }

    private void UpdateSpeedText()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        EnemyController enemy = FindObjectOfType<EnemyController>();

        if (playerSpeedText != null && enemySpeedText)
        {
            playerSpeedText.text = $"Player Speed: {player.moveSpeed}";
            enemySpeedText.text = $"Enemy Speed: {enemy.moveSpeed}";
        }
    }
}
