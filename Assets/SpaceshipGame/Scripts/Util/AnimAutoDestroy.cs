using UnityEngine;
using System.Collections;

public class AnimAutoDestroy : MonoBehaviour
{
    float _length;
    float _counter = 0;

    void Start()
    {
        //�R���|�[�l���g���擾
        Animator anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found", gameObject);
            return;
        }
        // �K�v�ȏ����擾
        AnimatorStateInfo infAnim = anim.GetCurrentAnimatorStateInfo(0);
        _length = infAnim.length;
    }

    void Update()
    {
        _counter += Time.deltaTime;
        if (_counter > _length)
        {
            Destroy(gameObject);
        }
    }
}