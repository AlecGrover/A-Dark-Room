using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneLoader : MonoBehaviour
{

    private bool _loadingGame = false;
    public GameObject LoadingBlackScreen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNewGame()
    {
        if (_loadingGame) return;
        _loadingGame = true;
        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {

        if (LoadingBlackScreen) LoadingBlackScreen.SetActive(true);

        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            Debug.Log($"Loading at {asyncOperation.progress * 100}%");
            if (asyncOperation.progress >= 0.90f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    public void QuitGame()
    {
        Application.Quit(1);
    }



}
