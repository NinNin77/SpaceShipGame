using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngine : MonoBehaviour
{
    // パラメーター(非公開)
    [SerializeField] private float _power = 0f;
    [SerializeField] private float _maxPower = 1.0f;
    //プロパティ(公開)
    public float Power
    {
        get => _power;
        set
        {
            if (value < 0)
            {
                Debug.Log("マイナスの値は入力できません", this.gameObject);
                return;
            }
            if (value > _maxPower)
            {
                Debug.Log("MaxPower以上の値は入力できません", this.gameObject);
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
                Debug.Log("マイナスの値は入力できません", this.gameObject);
                return;
            }
            _maxPower = value;
        }
    }

    // アサイン
    EnginesCtrl _engineCtrl;
    //各エンジン
    Effect_EngineParticle _main1;
    Effect_EngineParticle _main2;
    Effect_EngineParticle _main3;
    Effect_EngineParticle _main4;

    private void Start()
    {
        // 初期化
        _engineCtrl = GetComponentInParent<EnginesCtrl>();
        _main1 = GameObject.Find("1_main").GetComponent<Effect_EngineParticle>();
        _main2 = GameObject.Find("2_main").GetComponent<Effect_EngineParticle>();
        _main3 = GameObject.Find("3_main").GetComponent<Effect_EngineParticle>();
        _main4 = GameObject.Find("4_main").GetComponent<Effect_EngineParticle>();
    }
    public void Effect_Forward()
    {
        Effect_ResetEngine();// リセット

        // エンジンの出力
        float myFloat = _engineCtrl._mainEngine_Particle * _power;
        _main1._particlePower = myFloat / 4;
        _main2._particlePower = myFloat / 4;
        _main3._particlePower = myFloat / 4;
        _main4._particlePower = myFloat / 4;
    }
    public void Effect_ResetEngine()
    {
        // エンジン出力をリセット
        _main1._particlePower = 0;
        _main2._particlePower = 0;
        _main3._particlePower = 0;
        _main4._particlePower = 0;
    }
}
