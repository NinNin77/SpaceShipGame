using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginesCtrl : MonoBehaviour
{
    //�p�����[�^ //�{���́A�ϐ����̂͊K����Engine�ɒu���Ă����āA��������Q��/�l�ύX�o����悤�ɂ���̂��ǂ��B
    [SerializeField] public float _mainEngine_Move = 1000.0f;
    [SerializeField] public float _mainEngine_Particle = 100.0f;

    [SerializeField] public float _subEngine_Move = 500.0f;
    [SerializeField] public float _subEngine_Particle = 50.0f;

    // �A�T�C��
    Rigidbody2D _thisRiBdy;
    InputCtrl _coreKey;
    MainEngine _mainEngine;
    SubEngine _subEngine;

    //�ϐ�
    float _tmpPower = 0f;

    private void Start()
    {
        // �A�T�C��
        _thisRiBdy = GetComponent<Rigidbody2D>();
        _coreKey = GetComponent<InputCtrl>();
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

        //����
        _tmpPower = _subEngine.Power * _subEngine_Move;

        if (_coreKey._inputDirection == InputCtrl.InputDirection.Front)
        {
            _thisRiBdy.AddForce(transform.up * _tmpPower * Time.deltaTime, ForceMode2D.Force);//�����I
            _subEngine.Effect_Forward();
        }
        if (_coreKey._inputDirection == InputCtrl.InputDirection.Back)
        {
            _thisRiBdy.AddForce(-transform.up * _tmpPower * Time.deltaTime, ForceMode2D.Force);//�����I
            _subEngine.Effect_Backward();
        }
        if (_coreKey._inputDirection == InputCtrl.InputDirection.Right)
        {
            _thisRiBdy.AddTorque(-_tmpPower * Time.deltaTime, ForceMode2D.Force);//�����I
            _subEngine.Effect_TurnRight();
        }
        if (_coreKey._inputDirection == InputCtrl.InputDirection.Left)
        {
            _thisRiBdy.AddTorque(_tmpPower * Time.deltaTime, ForceMode2D.Force);//�����I
            _subEngine.Effect_TrunLeft();
        }
        if (_coreKey._inputDirection == InputCtrl.InputDirection.None)
        {
            _subEngine.Effect_ResetEngine();
        }
    }
    void MainEngine()
    {
        // _mainEngine
        //�L�[���͂��A�o�͂ɁA����
        _mainEngine.Power += _coreKey._inputMainEngineAddPower;
        _coreKey._inputMainEngineAddPower = 0; //���Z�b�g

        //�����I
        _tmpPower = _mainEngine.Power * _mainEngine_Move;
        if (_coreKey._inputDirection == InputCtrl.InputDirection.Front && _coreKey._inputMainEngineIsOn == true)
        {
            _thisRiBdy.AddForce(transform.up * _tmpPower * Time.deltaTime, ForceMode2D.Force);
        }

        //Effect
        if (_coreKey._inputDirection == InputCtrl.InputDirection.Front && _coreKey._inputMainEngineIsOn == true)
        {
            _mainEngine.Effect_Forward();
        }
        else
        {
            _mainEngine.Effect_ResetEngine();
        }
    }
}