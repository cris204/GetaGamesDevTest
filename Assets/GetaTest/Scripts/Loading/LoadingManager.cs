using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("UI")]
    public Image fillBar;
    public TextMeshProUGUI tittle;

    private bool isDone;
    private bool addedDelay;
    private AsyncOperation loadSceneOperation;

    void Start()
    {
        isDone = false;
        if (Env.CurrentScene == Env.MENU_SCENE) {
            Env.NextScene = Env.GAME_SCENE;
        } else if(Env.CurrentScene == Env.GAME_SCENE) {
            Env.NextScene = Env.MENU_SCENE;

        }

        loadSceneOperation = LoadingSceneManager.Instance.LoadNewSceneAsyncronously(Env.NextScene);
        loadSceneOperation.allowSceneActivation = false;
       
    }

    // Update is called once per frame
    void Update()
    {

        if (fillBar.fillAmount >= 0.9f) {
           
            if (!addedDelay) {
                addedDelay = true;
                StartCoroutine(Wait());
            }

            if (Input.anyKey && isDone) {
                loadSceneOperation.allowSceneActivation = true;
                LoadingSceneManager.Instance.UnloadLoadScene(Env.LOADING_SCENE);
            }

        } else {
            fillBar.fillAmount = loadSceneOperation.progress;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        fillBar.fillAmount = 1;
        isDone = true;
        tittle.text = "Press any key to continue";
    }

}
