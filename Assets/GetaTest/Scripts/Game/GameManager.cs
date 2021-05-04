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

    public Collider finalLine;

    [SerializeField]
    private GameState currentGameState;

    [Header("Timer")]
    private float lapTime = 20000;

    public float LapTime { get => lapTime; set => lapTime = value; }
    public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }

    private void Awake()
    {
        if(instance == null) {
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
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()){
            EventManager.Instance.RemoveListener<OnChangeGameStateEvent>(this.OnChangeGameState);
            EventManager.Instance.RemoveListener<OnDetectMidLineEvent>(this.OnDetectMidLine);
            EventManager.Instance.RemoveListener<OnDetectHourglassEvent>(this.OnDetectHourglass);
        }
    }


    public void Update()
    {
        if (currentGameState == GameState.Playing) {
            lapTime -= Time.deltaTime;

            if (lapTime <= 0 ) {
                lapTime = 0;
                currentGameState = GameState.TimeOut;
                EventManager.Instance.TriggerEvent(new OnChangeGameStateEvent
                {
                    gameState = currentGameState
                });
            }
        }
    }

    #region Events
    private void OnChangeGameState(OnChangeGameStateEvent e)
    {
        currentGameState = e.gameState;
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
