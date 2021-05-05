using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : Singleton<BackgroundMusic>
{
    public AudioSource audioSource;

    public void SetAudioClip(string name, float volume = 0.5f)
    {
        audioSource.clip = ResourcesManager.Instance.GetAudioClip(string.Format(Env.AUDIO_CLIP_PATH, name));
        audioSource.volume = volume;
        audioSource.Play();
    }

}
