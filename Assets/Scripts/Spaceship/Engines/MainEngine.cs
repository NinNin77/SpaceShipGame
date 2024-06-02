using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngine : MonoBehaviour
{
    // �p�����[�^�[
    [SerializeField] public float power = 0f;
    [SerializeField] public float maxPower = 1.0f;

    // �A�T�C��
    CtrlEngines CoreEngines;
    //�e�G���W��
    Effect_EngineParticle main1;
    Effect_EngineParticle main2;
    Effect_EngineParticle main3;
    Effect_EngineParticle main4;

    private void Start()
    {
        // ������
        CoreEngines = GetComponentInParent<CtrlEngines>();
        main1 = GameObject.Find("1_main").GetComponent<Effect_EngineParticle>();
        main2 = GameObject.Find("2_main").GetComponent<Effect_EngineParticle>();
        main3 = GameObject.Find("3_main").GetComponent<Effect_EngineParticle>();
        main4 = GameObject.Find("4_main").GetComponent<Effect_EngineParticle>();
    }

    //void EngineControl()
    //{

    //}
    public void Effect_Forward()
    {
        Effect_ResetEngine();// ���Z�b�g

        // �G���W���̏o��
        float myFloat = CoreEngines._mainEngine_Particle * power;
        main1.enginePower = myFloat / 4;
        main2.enginePower = myFloat / 4;
        main3.enginePower = myFloat / 4;
        main4.enginePower = myFloat / 4;
    }
    public void Effect_ResetEngine()
    {
        // �G���W���o�͂����Z�b�g
        main1.enginePower = 0;
        main2.enginePower = 0;
        main3.enginePower = 0;
        main4.enginePower = 0;
    }
}
