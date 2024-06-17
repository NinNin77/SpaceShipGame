using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngine : MonoBehaviour
{
    // パラメーター
    [SerializeField] public float power = 0f;
    [SerializeField] public float maxPower = 1.0f;

    // アサイン
    EnginesCtrl CoreEngines;
    //各エンジン
    Effect_EngineParticle main1;
    Effect_EngineParticle main2;
    Effect_EngineParticle main3;
    Effect_EngineParticle main4;

    private void Start()
    {
        // 初期化
        CoreEngines = GetComponentInParent<EnginesCtrl>();
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
        Effect_ResetEngine();// リセット

        // エンジンの出力
        float myFloat = CoreEngines._mainEngine_Particle * power;
        main1._enginePower = myFloat / 4;
        main2._enginePower = myFloat / 4;
        main3._enginePower = myFloat / 4;
        main4._enginePower = myFloat / 4;
    }
    public void Effect_ResetEngine()
    {
        // エンジン出力をリセット
        main1._enginePower = 0;
        main2._enginePower = 0;
        main3._enginePower = 0;
        main4._enginePower = 0;
    }
}
