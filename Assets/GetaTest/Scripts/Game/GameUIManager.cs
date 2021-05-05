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
    public TextMeshProUGUI endGameTittle;
    public GameObject endGameScreen;


    [Header("Start screen")]
    public TextMeshProUGUI startTittle;
    public GameObject startScreen;
    private float timeToStart = 3;

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
        if(GameManager.Instance.CurrentGameState == GameState.Waiting) {
            
            StartScreenCountDown();

        }else if (GameManager.Instance.CurrentGameState == GameState.Playing) {
            
            timer.text = Env.SecondsToMinutes(GameManager.Instance.LapTime);
        
        }
    }

    public void StartScreenCountDown()
    {
        if (timeToStart > 0 && timeToStart <= 1) {
            timeToStart -= Time.deltaTime;
            startTittle.text = "Go!";

        } else if (timeToStart <= 0) {
            startScreen.SetActive(false);
            Env.ThrowAudio("CheckPoint", 0.5f);
            EventManager.Instance.TriggerEvent(new OnChangeGameStateEvent
            {
                gameState = GameState.Playing
            });

        } else { 
            timeToStart -= Time.deltaTime;
            startTittle.text = timeToStart.ToString("F0");
        }


    }

    private void ShowEndGameScreen(bool playerWin)
    {
        endGameScreen.SetActive(true);
        if (playerWin) {
            endGameTittle.text = "Player wins";
        } else {
            endGameTittle.text = "Try again";
        }
    }

    #region Buttons
    // Called by button in scene
    public void BackToMenu()
    {
        GameManager.Instance.SaveInformation();
        LoadingSceneManager.Instance.LoadScene(Env.LOADING_SCENE);
    }
    #endregion

    #region Events

    private void OnChangeGameState(OnChangeGameStateEvent e)
    {
         if (e.gameState == GameState.Winner) {
            ShowEndGameScreen(true);
        } else if (e.gameState == GameState.TimeOut) {
            ShowEndGameScreen(false);
        }
    }


    #endregion
}
