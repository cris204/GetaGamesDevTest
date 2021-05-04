
using UnityEngine;

public static class Env 
{
    public const string GAME_SCENE = "TrackScene";
    public const string MENU_SCENE = "MainMenuScene";
    public const string LOADING_SCENE = "LoadingScene";

    public static string CurrentScene = "";
    public static string NextScene = "";

    ////// Path

    public const string HOURGLASS_GAMEOBJECT_PATH = "Prefabs/Hourglass";
    public const string GENRIC_GAMEOBJECT_PATH = "Prefabs/{0}";


    //KEY

    public const string RACING_TIMES_KEY= "RacingTimesKey";
    public const string WIN_TIMES_KEY = "WinsTimesKey";
    public const string BEST_LAP_TIME_KEY = "BestLapTimeKey";



    public static string SecondsToMinutes(float currentTime)
    {
        float minutes = Mathf.Floor(currentTime / 60);
        float seconds = currentTime % 60;

        return minutes + ":" + Mathf.RoundToInt(seconds);
    }

}
