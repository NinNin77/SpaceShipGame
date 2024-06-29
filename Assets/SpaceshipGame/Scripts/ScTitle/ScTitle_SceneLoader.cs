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

    /// <summary> �񓯊��ȃV�[�����[�h���A�V�[���ڍs���Ă����֐� </summary>
    /// <param name="sceneName">�V�[����</param>
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // ���݂̃V�[�����擾
        string lastSceneNm = SceneManager.GetActiveScene().name;

        // ���̃V�[����񓯊��œǂݍ���
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)�@//���[�h�I���܂őҋ@
        {
            yield return null;
        }

        // �O�̃V�[�����A�����[�h
        SceneManager.UnloadSceneAsync(lastSceneNm);
    }
}
