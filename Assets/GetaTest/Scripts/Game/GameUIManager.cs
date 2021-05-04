using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUIManager : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timer;
    private float minutes;
    private float seconds;

    [Header("EndGame screen")]
    public TextMeshProUGUI tittle;
    public GameObject endGameScreen;

    private void Start()
    {
        EventManager.Instance.AddListener<OnChangeGameStateEvent>(this.OnChangeGameState);
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<OnChangeGameStateEvent>(this.OnChangeGameState);
        }
    }


    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Playing) {
            timer.text = SecondsToMinutes(GameManager.Instance.LapTime);
        }
    }

    private string SecondsToMinutes(float currentTime)
    {
        minutes = Mathf.Floor(currentTime / 60);
        seconds = currentTime % 60;

        return  minutes + ":" + Mathf.RoundToInt(seconds);
    }

    private void ShowEndGameScreen(bool playerWin)
    {
        endGameScreen.SetActive(true);
        if (playerWin) {
            tittle.text = "Player wins";
        } else {
            tittle.text = "Try again";
        }
    }

#region Events

    private void OnChangeGameState(OnChangeGameStateEvent e)
    {
       if(e.gameState == GameState.Winner) {
            ShowEndGameScreen(true);
       }else if(e.gameState == GameState.TimeOut) {
            ShowEndGameScreen(false);
        }
    }


    #endregion
}
