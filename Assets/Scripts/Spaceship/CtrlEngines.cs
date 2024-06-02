using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlEngines : MonoBehaviour
{
    //�p�����[�^ //�{���́A�ϐ����̂͊K����Engine�ɒu���Ă����āA��������Q��/�l�ύX�o����悤�ɂ���̂��ǂ��B
    [SerializeField] public float _mainEngine_Move = 1000.0f;
    [SerializeField] public float _mainEngine_Particle = 200.0f;

    [SerializeField] public float _subEngine_Move = 500.0f;
    [SerializeField] public float _subEngine_Particle = 100.0f;

    // �A�T�C��
    Rigidbody2D _thisRiBdy;
    CtrlInput _coreKey;
    MainEngine _mainEngine;
    SubEngine _subEngine;

    //�ϐ�
    float _tmpPower = 0f;

    private void Start()
    {
        // �A�T�C��
        _thisRiBdy = GetComponent<Rigidbody2D>();
        _coreKey = GetComponent<CtrlInput>();
        _mainEngine = GetComponentInChildren<MainEngine>();
        _subEngine = GetComponentInChildren<SubEngine>();
    }

    void Update()
    {
        SubEngine();
        MainEngine();
    }

    void SubEngine()
    {
        // _subEngine
        _subEngine.power = 1.0f;
        _tmpPower = _subEngine.power * _subEngine_Move;

        if (_coreKey.inputDirection == CtrlInput.InputDirection.Front)
        {
            _thisRiBdy.AddForce(transform.up * _tmpPower * Time.deltaTime, ForceMode2D.Force);//�����I
            _subEngine.Effect_Forward();
        }
        if (_coreKey.inputDirection == CtrlInput.InputDirection.Back)
        {
            _thisRiBdy.AddForce(-transform.up * _tmpPower * Time.deltaTime, ForceMode2D.Force);//�����I
            _subEngine.Effect_Backward();
        }
        if (_coreKey.inputDirection == CtrlInput.InputDirection.Right)
        {
            _thisRiBdy.AddTorque(-_tmpPower * Time.deltaTime, ForceMode2D.Force);//�����I
            _subEngine.Effect_TurnRight();
        }
        if (_coreKey.inputDirection == CtrlInput.InputDirection.Left)
        {
            _thisRiBdy.AddTorque(_tmpPower * Time.deltaTime, ForceMode2D.Force);//�����I
            _subEngine.Effect_TrunLeft();
        }
        if (_coreKey.inputDirection == CtrlInput.InputDirection.None)
        {
            _subEngine.Effect_ResetEngine();
        }
    }
    void MainEngine()
    {
        // _mainEngine
        //�L�[���͂��A�o�͂ɁA����
        _mainEngine.power += _coreKey.inputMainEngineAddPower;
        _coreKey.inputMainEngineAddPower = 0; //���Z�b�g

        //�G���W���o�͂ɏ���Ɖ�����݂���
        if (_mainEngine.power > _mainEngine.maxPower)
        {
            _mainEngine.power = _mainEngine.maxPower;
        }
        else if(_mainEngine.power < 0)
        {
            _mainEngine.power = 0;
        }

        //�����I
        _tmpPower = _mainEngine.power * _mainEngine_Move;
        if (_coreKey.inputDirection == CtrlInput.InputDirection.Front && _coreKey.inputMainEngineIsOn == true)
        {
            _thisRiBdy.AddForce(transform.up * _tmpPower * Time.deltaTime, ForceMode2D.Force);
        }

        //Effect
        if (_coreKey.inputDirection == CtrlInput.InputDirection.Front && _coreKey.inputMainEngineIsOn == true)
        {
            _mainEngine.Effect_Forward();
        }
        else
        {
            _mainEngine.Effect_ResetEngine();
        }
    }
}
