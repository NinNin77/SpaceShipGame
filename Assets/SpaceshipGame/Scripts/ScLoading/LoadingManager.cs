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
        // ���̃V�[����񓯊��œǂݍ���
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("InGame");

        while (!asyncLoad.isDone) //�I���܂ő҂�
        {
            yield return null;
        }

        // �O�̃��[�h�V�[�����A�����[�h
        SceneManager.UnloadSceneAsync("Loading");
    }
}
