using UnityEngine;
using System.Collections;

public class AnimAutoDestroy : MonoBehaviour
{
    private float _length;
    private float _counter = 0;

    void Start()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo infAnim = anim.GetCurrentAnimatorStateInfo(0);
        _length = infAnim.length;
    }

    void Update()
    {
        _counter += Time.deltaTime;
        if (_counter > _length)
        {
            GameObject.Destroy(gameObject);
        }
    }
}