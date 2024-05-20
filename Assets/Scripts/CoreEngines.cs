using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEngines : MonoBehaviour
{
    //パラメータ //本当は、変数自体は階下のEngineに置いておいて、ここから参照/値変更出来るようにするのが良い。
    [SerializeField] public float MainEngine_Move = 500.0f;
    [SerializeField] public float MainEngine_Particle = 100.0f;

    [SerializeField] public float SubEngine_Move = 500.0f;
    [SerializeField] public float SubEngine_Particle = 100.0f;

    [SerializeField] public float test = 0f;

    // アサイン
    Rigidbody2D ThisRiBdy;
    CoreKey CoreKey;
    MainEngine MainEngine;
    SubEngine SubEngine;

    //変数
    float tmp_Power = 0f;

    private void Start()
    {
        // アサイン
        ThisRiBdy = GetComponent<Rigidbody2D>();
        CoreKey = GetComponent<CoreKey>();
        MainEngine = GetComponentInChildren<MainEngine>();
        SubEngine = GetComponentInChildren<SubEngine>();
    }

    void Update()
    {
        CORESubEngine();
        COREMainEngine();
    }

    void CORESubEngine()
    {
        // SubEngine
        SubEngine.power = 1.0f;
        tmp_Power = SubEngine.power * SubEngine_Move;

        if (CoreKey.inputDirection == CoreKey.InputDirection.Front)
        {
            ThisRiBdy.AddForce(transform.up * tmp_Power * Time.deltaTime, ForceMode2D.Force);//動け！
            SubEngine.Effect_Forward();
        }
        if (CoreKey.inputDirection == CoreKey.InputDirection.Back)
        {
            ThisRiBdy.AddForce(-transform.up * tmp_Power * Time.deltaTime, ForceMode2D.Force);//動け！
            SubEngine.Effect_Backward();
        }
        if (CoreKey.inputDirection == CoreKey.InputDirection.Right)
        {
            ThisRiBdy.AddTorque(-tmp_Power * Time.deltaTime, ForceMode2D.Force);//動け！
            SubEngine.Effect_TurnRight();
        }
        if (CoreKey.inputDirection == CoreKey.InputDirection.Left)
        {
            ThisRiBdy.AddTorque(tmp_Power * Time.deltaTime, ForceMode2D.Force);//動け！
            SubEngine.Effect_TrunLeft();
        }
        if (CoreKey.inputDirection == CoreKey.InputDirection.None)
        {
            SubEngine.Effect_ResetEngine();
        }
    }
    void COREMainEngine()
    {
        // MainEngine
        //キー入力を、出力に、足す
        MainEngine.power += CoreKey.inputMainEngineAddPower;
        CoreKey.inputMainEngineAddPower = 0; //リセット

        //エンジン出力に上限と下限を設ける
        if (MainEngine.power > MainEngine.maxPower)
        {
            MainEngine.power = MainEngine.maxPower;
        }
        else if(MainEngine.power < 0)
        {
            MainEngine.power = 0;
        }

        //動け！
        tmp_Power = MainEngine.power * MainEngine_Move;
        if (CoreKey.inputDirection == CoreKey.InputDirection.Front && CoreKey.inputMainEngineIsOn == true)
        {
            ThisRiBdy.AddForce(transform.up * tmp_Power * Time.deltaTime, ForceMode2D.Force);
        }

        //Effect

    }
}
