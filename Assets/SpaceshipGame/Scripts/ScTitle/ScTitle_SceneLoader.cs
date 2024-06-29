using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScTitle_SceneLoader : MonoBehaviour
{

    public void LoadSceneAsyncScLoading()
    {
        Debug.Log("LoadScLoadingAsync");
        StartCoroutine(LoadSceneAsync("Loading"));
    }

    /// <summary> 非同期なシーンロードし、シーン移行してくれる関数 </summary>
    /// <param name="sceneName">シーン名</param>
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // 現在のシーンを取得
        string lastSceneNm = SceneManager.GetActiveScene().name;

        // 次のシーンを非同期で読み込み
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)　//ロード終わるまで待機
        {
            yield return null;
        }

        // 前のシーンをアンロード
        SceneManager.UnloadSceneAsync(lastSceneNm);
    }
}
