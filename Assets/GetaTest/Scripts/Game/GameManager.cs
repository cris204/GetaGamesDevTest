using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { 

    Waiting,
    Playing,
    Pause,
    Winner,
    TimeOut
    
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {
        get
        {
            return instance;
        }
    }

    [Header("Data")]
    public int racingPlayed;
    public int racingWins;
    public float bestLap = float.MaxValue;

    public Collider finalLine;

    [SerializeField]
    private GameState currentGameState;

    [Header("Timer")]
    private float lapTime = 20000;
    private float currentLapTime;

    public float LapTime { get => lapTime; set => lapTime = value; }
    public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        EventManager.Instance.AddListener<OnChangeGameStateEvent>(this.OnChangeGameState);
        EventManager.Instance.AddListener<OnDetectMidLineEvent>(this.OnDetectMidLine);
        EventManager.Instance.AddListener<OnDetectHourglassEvent>(this.OnDetectHourglass);
        currentGameState = GameState.Playing;
        Env.CurrentScene = Env.GAME_SCENE;
        LoadInformation();
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<OnChangeGameStateEvent>(this.OnChangeGameState);
            EventManager.Instance.RemoveListener<OnDetectMidLineEvent>(this.OnDetectMidLine);
            EventManager.Instance.RemoveListener<OnDetectHourglassEvent>(this.OnDetectHourglass);
        }
    }


    public void Update()
    {
        if (currentGameState == GameState.Playing) {
            lapTime -= Time.deltaTime;
            currentLapTime += Time.deltaTime;

            if (lapTime <= 0) {
                lapTime = 0;
                currentGameState = GameState.TimeOut;
                EventManager.Instance.TriggerEvent(new OnChangeGameStateEvent
                {
                    gameState = currentGameState
                });
            }
        }
    }

    public void SaveInformation()
    {
        StorageManager.Instance.SetInt(Env.RACING_TIMES_KEY, racingPlayed);
        StorageManager.Instance.SetInt(Env.WIN_TIMES_KEY, racingWins);
        StorageManager.Instance.SetFloat(Env.BEST_LAP_TIME_KEY, currentLapTime);
    }

    private void LoadInformation()
    {
        racingPlayed = StorageManager.Instance.GetInt(Env.RACING_TIMES_KEY, 0);
        racingWins = StorageManager.Instance.GetInt(Env.WIN_TIMES_KEY, 0);
        bestLap = StorageManager.Instance.GetFloat(Env.BEST_LAP_TIME_KEY);
        if(bestLap == 0) {
            bestLap = float.MaxValue;
        }
    }


    #region Events
    private void OnChangeGameState(OnChangeGameStateEvent e)
    {
        currentGameState = e.gameState;
        if(currentGameState == GameState.TimeOut) {
            racingPlayed++;
        }

        if(currentGameState == GameState.Winner) {
            racingPlayed++;
            racingWins++;
            if(currentLapTime <= bestLap) {
                bestLap = currentLapTime;
            }
        }
    }
    private void OnDetectMidLine(OnDetectMidLineEvent e)
    {
        finalLine.enabled = true;
    }


    private void OnDetectHourglass(OnDetectHourglassEvent e)
    {
        lapTime += 10f;
    }


    #endregion

}
