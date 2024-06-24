using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HyperspaceCtrl : MonoBehaviour
{
    //�p�����[�^
    [SerializeField] public float _maxTimer = 180.0f;
    public float _currentTimer = 0.0f;
    private float _1sCounter = 0.0f;

    private void Start()
    {
        _currentTimer = _maxTimer;
    }

    void Update()
    {
        _currentTimer -= Time.deltaTime;
        _1sCounter += Time.deltaTime;

        // ��b���ƂɌĂяo�����(���S�ɐ��m�ł͂Ȃ�)
        if (_1sCounter > 1.0f )
        {
            _1sCounter -= 1.0f;

            var tmp = Math.Floor(_currentTimer * 100); //������2�ʂŁA�؂�̂�
            tmp = tmp / 100; //���Ƃ͕ʂ̍s�ňʂ�߂��Ȃ��ƁA�Ȃ����o�O��B
            //Debug.Log("���� " + tmp + "�@�b�o��");
        }
    }
}
