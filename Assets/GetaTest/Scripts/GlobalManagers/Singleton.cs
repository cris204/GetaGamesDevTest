using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null) {
                CreateInstance();
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null) {
            CreateInstance();
        } else {
            Destroy(gameObject);
        }
    }

    public static void Init()
    {
        if (instance == null) {
            CreateInstance();
        }
    }

    private static void CreateInstance()
    {
        GameObject newObject = GameObject.Find($"{typeof(T).Name}");
        if (newObject == null) {
            newObject = new GameObject($"{typeof(T).Name}");
            instance = newObject.AddComponent<T>();
            DontDestroyOnLoad(newObject);
        } else {
            instance = newObject.GetComponent<T>();
            DontDestroyOnLoad(newObject);
        }
    }

    public static bool HasInstance()
    {
        return instance != null;
    }
}
