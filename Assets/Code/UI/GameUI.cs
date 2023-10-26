using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Text label;

    [SerializeField]
    private Text enemiesLabel;
    
    [SerializeField]
    private string gameOverMessage = "You died!";

    private void OnEnable()
    {
        GameEvents.onGameStatsChange.AddListener(UpdateEnemyLabel);
        GameEvents.onGameStart.AddListener(UpdateEnemyLabel);

        GameEvents.onPlayerDeath.AddListener(ShowLastMessage);
    }

    private void OnDisable()
    {
        GameEvents.onGameStatsChange.RemoveListener(UpdateEnemyLabel);
        GameEvents.onGameStart.RemoveListener(UpdateEnemyLabel);
        
        GameEvents.onPlayerDeath.RemoveListener(ShowLastMessage);
    }
    
    void Start()
    {
        UpdateEnemyLabel();
    }

    void UpdateEnemyLabel()
    {
        int livDragons = GameStats.Instance.EnemyCount;
        int dedDragons = GameStats.Instance.Kills;
        
        enemiesLabel.text = "Enemies: " + livDragons + "\nKilled: " + dedDragons;
    }

    void ShowLastMessage()
    {
        StartCoroutine(ShowTextAneQuitCoroutine(gameOverMessage));
    }

    IEnumerator ShowTextAneQuitCoroutine(string message)
    {
        yield return new WaitForSeconds(0.3f);
        ShowLabel(message);
        yield return new WaitForSeconds(0.5f);
        
        GameEvents.onGameEnd.Invoke();
    }

    void ShowLabel(string message)
    {
        label.text = message;
        label.gameObject.SetActive(true);
    }
    
}