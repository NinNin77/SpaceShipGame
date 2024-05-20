using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngine : MonoBehaviour
{
    // �p�����[�^�[
    [SerializeField] public float power = 0f;
    [SerializeField] public float maxPower = 1.0f;

    // �A�T�C��
    CoreEngines CoreEngines;
    //�e�G���W��
    main_1 main_1;

    private void Start()
    {
        // ������
        CoreEngines = GetComponentInParent<CoreEngines>();
        main_1 = GetComponentInChildren<main_1>();
    }

    //void EngineControl()
    //{

    //}
    public void Effect_Forward()
    {
        Effect_ResetEngine();// ���Z�b�g

        // �G���W���̏o��
        float myFloat = CoreEngines.MainEngine_Particle;
        main_1.enginePower = myFloat;
        //BtmRight.enginePower = myFloat / 4;
        //BtmLeft.enginePower = myFloat / 4;
        //SideRight.enginePower = myFloat / 4;
        //SideLeft.enginePower = myFloat / 4;
    }
    public void Effect_ResetEngine()
    {
        // �G���W���o�͂����Z�b�g
        main_1.enginePower = 0;
        //TopRight.enginePower = 0;
        //TopLeft.enginePower = 0;
        //BtmRight.enginePower = 0;
        //BtmLeft.enginePower = 0;
        //SideRight.enginePower = 0;
        //SideLeft.enginePower = 0;
    }
}
