using System.Collections;
using UnityEngine;

public class AudioAutoDestroy : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        //コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found", gameObject);
            return;
        }

        // コルーチンを開始
        StartCoroutine(DestroyAfterPlaying());
    }

    IEnumerator DestroyAfterPlaying()
    {
        //プレイが終わるまで待機
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Destroy
        Destroy(gameObject);
    }
}
