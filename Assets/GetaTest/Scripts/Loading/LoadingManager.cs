using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("Bar")]
    public Image fillBar;

    private AsyncOperation loadSceneOperation;

    void Start()
    {
        if(Env.CurrentScene == Env.MENU_SCENE) {
            Env.NextScene = Env.GAME_SCENE;
        } else if(Env.CurrentScene == Env.GAME_SCENE) {
            Env.NextScene = Env.MENU_SCENE;

        }

        loadSceneOperation = LoadingSceneManager.Instance.LoadNewSceneAsyncronously(Env.NextScene);
    }

    // Update is called once per frame
    void Update()
    {
        fillBar.fillAmount = loadSceneOperation.progress;
        if(fillBar.fillAmount >= 0.9f) {
            LoadingSceneManager.Instance.UnloadLoadScene(Env.LOADING_SCENE);
        }
    }
}
