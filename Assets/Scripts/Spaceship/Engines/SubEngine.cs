using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �e�G���W���𐧌䂷�邽�߂́A
/// �T��ނ̊֐����܂Ƃ߂��Ă��
/// </summary>
public class SubEngine : MonoBehaviour
{
    [SerializeField] private float _power = 0f;
    [SerializeField] private float _maxPower = 1.0f;
    //�v���p�e�B(���J)
    public float Power
    {
        get => _power;
        set
        {
            if (value < 0)
            {
                Debug.Log("�}�C�i�X�̒l�͓��͂ł��܂���", this.gameObject);
                return;
            }
            if (value > _maxPower)
            {
                Debug.Log("MaxPower�ȏ�̒l�͓��͂ł��܂���", this.gameObject);
                return;
            }
            _power = value;
        }
    }
    public float MaxPower
    {
        get => _maxPower;
        set
        {
            if (value <= 0)
            {
                Debug.Log("�}�C�i�X�̒l�͓��͂ł��܂���", this.gameObject);
                return;
            }
            _maxPower = value;
        }
    }

    // �A�T�C���@
    EnginesCtrl _enginesCtrl;
    //�e�G���W����o�^
    Effect_EngineParticle _topRight;
    Effect_EngineParticle _topLeft;
    Effect_EngineParticle _btmRight;
    Effect_EngineParticle _btmLeft;
    Effect_EngineParticle _sideRight;
    Effect_EngineParticle _sideLeft;

    private void Start()
    {
        // ������
        _enginesCtrl = GetComponentInParent<EnginesCtrl>();
        _topRight = GameObject.Find("1_TopRight").GetComponent<Effect_EngineParticle>();
        _topLeft = GameObject.Find("2_TopLeft").GetComponent<Effect_EngineParticle>();
        _btmRight = GameObject.Find("3_BtmRight").GetComponent<Effect_EngineParticle>();
        _btmLeft = GameObject.Find("4_BtmLeft").GetComponent<Effect_EngineParticle>();
        _sideRight = GameObject.Find("5_SideRight").GetComponent<Effect_EngineParticle>();
        _sideLeft = GameObject.Find("6_SideLeft").GetComponent<Effect_EngineParticle>();
    }
    public void Effect_Forward()
    {
        Effect_ResetEngine();// ���Z�b�g

        // �G���W���̏o��
        float myFloat = _enginesCtrl._subEngine_Particle * _power;
        _btmRight._particlePower = myFloat / 4;
        _btmLeft._particlePower = myFloat / 4;
        _sideRight._particlePower = myFloat / 4;
        _sideLeft._particlePower = myFloat / 4;

        //�G���W���̌���
        _btmRight.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        _btmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        _sideRight.transform.localRotation = Quaternion.Euler(0f, 0f, -90);
        _sideLeft.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
    }
    public void Effect_Backward()
    {
        Effect_ResetEngine();// ���Z�b�g

        // �G���W���̏o��
        float myFloat = _enginesCtrl._subEngine_Particle * _power;
        _topRight._particlePower = myFloat / 4;
        _topLeft._particlePower = myFloat / 4;
        _sideRight._particlePower = myFloat / 4;
        _sideLeft._particlePower = myFloat / 4;

        //�G���W���̌���
        _topRight.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        _topLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        _sideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        _sideLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
    }
    public void Effect_TurnRight()
    {
        Effect_ResetEngine();// ���Z�b�g

        //�G���W���̏o��
        float myFloat = _enginesCtrl._subEngine_Particle * _power;
        _topLeft._particlePower = myFloat / 4;
        _btmRight._particlePower = myFloat / 4;
        _sideRight._particlePower = myFloat / 4;
        _sideLeft._particlePower = myFloat / 4;

        //�G���W���̌���
        //_topRight.transform.localRotation = Quaternion.Euler(0f, 0f, 135f);
        _topLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 215f); //90 + 90 + 45 - 10
        _btmRight.transform.localRotation = Quaternion.Euler(0f, 0f, 35f);//270 + 90 + 45 -10
        //_btmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 315f);
        _sideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 80f);//90 - 10
        _sideLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 260f);//270 - 10
    }
    public void Effect_TrunLeft()
    {
        Effect_ResetEngine();// ���Z�b�g

        //�G���W���̏o��
        float myFloat = _enginesCtrl._subEngine_Particle * _power;
        _topRight._particlePower = myFloat / 4;
        _btmLeft._particlePower = myFloat / 4;
        _sideRight._particlePower = myFloat / 4;
        _sideLeft._particlePower = myFloat / 4;

        //�G���W���̌���
        _topRight.transform.localRotation = Quaternion.Euler(0f, 0f, 325f);//270 + 45 + 10
        //_topLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 45f);
        //_btmRight.transform.localRotation = Quaternion.Euler(0f, 0f, 225f);
        _btmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 145f);//90 + 45 + 10
        _sideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 280f); //270 + 10
        _sideLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 100f); //90 + 10
    }
    public void Effect_ResetEngine()
    {
        // �G���W���o�͂����Z�b�g
        _topRight._particlePower = 0;
        _topLeft._particlePower = 0;
        _btmRight._particlePower = 0;
        _btmLeft._particlePower = 0;
        _sideRight._particlePower = 0;
        _sideLeft._particlePower = 0;

        //�G���W���̌��������Z�b�g
        _topRight.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        _topLeft.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        _btmRight.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        _btmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        _sideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        _sideLeft.transform.localRotation = quaternion.Euler(0f, 0f, 180f);
    }
}
