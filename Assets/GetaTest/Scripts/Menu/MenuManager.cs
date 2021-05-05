using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI racingPlayedText;
    public TextMeshProUGUI racingWinsText;
    public TextMeshProUGUI fasterLapText;


    private void Start()
    {
        Env.CurrentScene = Env.MENU_SCENE;
        Env.NextScene = Env.LOADING_SCENE;
        LoadInformation();
        BackgroundMusic.Instance.SetAudioClip("Menu", 0.3f);
    }


    private void LoadInformation()
    {
        racingPlayedText.text = string.Format("Racings played: {0}", StorageManager.Instance.GetInt(Env.RACING_TIMES_KEY, 0));
        racingWinsText.text = string.Format("Racings Wins: {0}", StorageManager.Instance.GetInt(Env.WIN_TIMES_KEY, 0));
        
        if (StorageManager.Instance.GetFloat(Env.BEST_LAP_TIME_KEY) <= 0) {
            fasterLapText.text = string.Format("FasterLap: --");
        } else {
            fasterLapText.text = string.Format("FasterLap: {0}", Env.SecondsToMinutes(StorageManager.Instance.GetFloat(Env.BEST_LAP_TIME_KEY)));
        }
    }

    #region Buttons 

    //Called by UI button
    public void PlayGame()
    {
        LoadingSceneManager.Instance.LoadScene(Env.LOADING_SCENE);
    }

    #endregion

}
