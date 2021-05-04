using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        Env.CurrentScene = Env.MENU_SCENE;
        Env.NextScene = Env.LOADING_SCENE;
    }

    #region Buttons 

    //Called by UI button
    public void PlayGame()
    {
        LoadingSceneManager.Instance.LoadScene(Env.LOADING_SCENE);
    }

    #endregion

}
