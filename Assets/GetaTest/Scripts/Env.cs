
using UnityEngine;

public static class Env 
{
    public const string GAME_SCENE = "TrackScene";
    public const string MENU_SCENE = "MainMenuScene";
    public const string LOADING_SCENE = "LoadingScene";

    public static string CurrentScene = "";
    public static string NextScene = "";

    ////// Path

    public const string HOURGLASS_GAMEOBJECT_PATH = "Prefabs/Objects/Hourglass";
    public const string GENRIC_GAMEOBJECT_PATH = "Prefabs/Objects/{0}";
    public const string GENRIC_VFX_GAMEOBJECT_PATH = "Prefabs/VFX/{0}";
    public const string AUDIO_OBJECT_POOL_PATH = "Prefabs/Audio/AudioObject";
    public const string AUDIO_CLIP_PATH = "AudioClip/{0}";

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

    public static void ThrowAudio(string name, float volume = 0.25f)
    {
        AudioObject audio = PoolManager.Instance.GetObject(AUDIO_OBJECT_POOL_PATH).GetComponent<AudioObject>();
        audio.source.volume = volume;
        audio.source.clip = ResourcesManager.Instance.GetAudioClip(string.Format(AUDIO_CLIP_PATH, name));
        audio.Play();
    }


}
