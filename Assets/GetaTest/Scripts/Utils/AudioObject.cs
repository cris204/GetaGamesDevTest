using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour
{
    public AudioSource source;
    
    private void Awake() {
        this.source = this.gameObject.GetComponent<AudioSource>();
    }
    
    public void Play() {
        this.source.Play();
        if (!this.source.loop) {
            StartCoroutine(StopSound());
        }
    }

    private IEnumerator StopSound() {
        yield return new WaitForSeconds(source.clip.length);
        this.Stop();
    }

    public void Stop() {
        if (this.source.isPlaying) {
            this.source.Stop();
        }

        PoolManager.Instance.ReleaseObject(Env.AUDIO_OBJECT_POOL_PATH, this.gameObject);
    }
}
