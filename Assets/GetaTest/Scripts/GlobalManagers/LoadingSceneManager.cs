using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadingSceneManager : Singleton<LoadingSceneManager>
{
    /// <returns>Return true when scene finish to load</returns>
    public AsyncOperation LoadNewSceneAsyncronously(string sceneName, Action postLoad = null)
    {
        AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        StartCoroutine(LoadSceneAsyncronously(loadSceneOperation, sceneName, postLoad));
        return loadSceneOperation;
    }

    private IEnumerator LoadSceneAsyncronously(AsyncOperation loadSceneOperation, string sceneName, Action postLoad)
    {
        loadSceneOperation.allowSceneActivation = false;

        while (!loadSceneOperation.isDone) {
            if (loadSceneOperation.progress >= 0.9f) {
                loadSceneOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        if (postLoad != null) {
            postLoad();
        }
    }
    public void UnloadLoadScene(string name)
    {
        SceneManager.UnloadScene(name);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
