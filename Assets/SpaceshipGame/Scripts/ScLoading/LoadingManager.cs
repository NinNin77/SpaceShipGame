using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScLoadingManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadScInGameAsync());
    }

    IEnumerator LoadScInGameAsync()
    {
        // 次のシーンを非同期で読み込み
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("InGame");

        while (!asyncLoad.isDone) //終わるまで待て
        {
            yield return null;
        }

        // 前のロードシーンをアンロード
        SceneManager.UnloadSceneAsync("Loading");
    }
}
