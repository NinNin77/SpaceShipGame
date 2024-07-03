using System.Collections;
using UnityEngine;

public class AudioAutoDestroy : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        //�R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found", gameObject);
            return;
        }

        // �R���[�`�����J�n
        StartCoroutine(DestroyAfterPlaying());
    }

    IEnumerator DestroyAfterPlaying()
    {
        //�v���C���I���܂őҋ@
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Destroy
        Destroy(gameObject);
    }
}
