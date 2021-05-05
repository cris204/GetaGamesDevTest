using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourcesManager : Singleton<ResourcesManager>
{

    /// <summary>
    /// Used by pool
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string path)
    {
        return Resources.Load<GameObject>(path);
    }
    public Sprite GetSprite(string spritePath)
    {
        return Resources.Load<Sprite>(spritePath);
    }

    public Material GetMaterial(string materialPath)
    {
        return Resources.Load<Material>(materialPath);
    }

    public Texture2D GetTexture(string path)
    {
        return Resources.Load<Texture2D>(path);
    }
    public AudioClip GetAudioClip(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}
