using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 各エンジンを制御するための、
/// ５種類の関数がまとめられてるよ
/// </summary>
public class SubEngine : MonoBehaviour
{
    // パラメーター
    [SerializeField] public float power = 1f;
    [SerializeField] public float maxPower = 1.0f; //使ってない

    // アサイン　
    EnginesCtrl CoreEngines;
    //各エンジンを登録
    Effect_EngineParticle TopRight;
    Effect_EngineParticle TopLeft;
    Effect_EngineParticle BtmRight;
    Effect_EngineParticle BtmLeft;
    Effect_EngineParticle SideRight;
    Effect_EngineParticle SideLeft;

    private void Start()
    {
        // 初期化
        CoreEngines = GetComponentInParent<EnginesCtrl>();
        TopRight = GameObject.Find("1_TopRight").GetComponent<Effect_EngineParticle>();
        TopLeft = GameObject.Find("2_TopLeft").GetComponent<Effect_EngineParticle>();
        BtmRight = GameObject.Find("3_BtmRight").GetComponent<Effect_EngineParticle>();
        BtmLeft = GameObject.Find("4_BtmLeft").GetComponent<Effect_EngineParticle>();
        SideRight = GameObject.Find("5_SideRight").GetComponent<Effect_EngineParticle>();
        SideLeft = GameObject.Find("6_SideLeft").GetComponent<Effect_EngineParticle>();
    }

    public void Effect_Forward()
    {
        Effect_ResetEngine();// リセット

        // エンジンの出力
        float myFloat = CoreEngines._subEngine_Particle * power;
        BtmRight._enginePower = myFloat / 4;
        BtmLeft._enginePower = myFloat / 4;
        SideRight._enginePower = myFloat / 4;
        SideLeft._enginePower = myFloat / 4;

        //エンジンの向き
        BtmRight.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        BtmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        SideRight.transform.localRotation = Quaternion.Euler(0f, 0f, -90);
        SideLeft.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
    }
    public void Effect_Backward()
    {
        Effect_ResetEngine();// リセット

        // エンジンの出力
        float myFloat = CoreEngines._subEngine_Particle * power;
        TopRight._enginePower = myFloat / 4;
        TopLeft._enginePower = myFloat / 4;
        SideRight._enginePower = myFloat / 4;
        SideLeft._enginePower = myFloat / 4;

        //エンジンの向き
        TopRight.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        TopLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        SideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        SideLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
    }
    public void Effect_TurnRight()
    {
        Effect_ResetEngine();// リセット

        //エンジンの出力
        float myFloat = CoreEngines._subEngine_Particle * power;
        TopLeft._enginePower = myFloat / 4;
        BtmRight._enginePower = myFloat / 4;
        SideRight._enginePower = myFloat / 4;
        SideLeft._enginePower = myFloat / 4;

        //エンジンの向き
        //TopRight.transform.localRotation = Quaternion.Euler(0f, 0f, 135f);
        TopLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 215f); //90 + 90 + 45 - 10
        BtmRight.transform.localRotation = Quaternion.Euler(0f, 0f, 35f);//270 + 90 + 45 -10
        //BtmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 315f);
        SideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 80f);//90 - 10
        SideLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 260f);//270 - 10
    }
    public void Effect_TrunLeft()
    {
        Effect_ResetEngine();// リセット

        //エンジンの出力
        float myFloat = CoreEngines._subEngine_Particle * power;
        TopRight._enginePower = myFloat / 4;
        BtmLeft._enginePower = myFloat / 4;
        SideRight._enginePower = myFloat / 4;
        SideLeft._enginePower = myFloat / 4;

        //エンジンの向き
        TopRight.transform.localRotation = Quaternion.Euler(0f, 0f, 325f);//270 + 45 + 10
        //TopLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 45f);
        //BtmRight.transform.localRotation = Quaternion.Euler(0f, 0f, 225f);
        BtmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 145f);//90 + 45 + 10
        SideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 280f); //270 + 10
        SideLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 100f); //90 + 10
    }
    public void Effect_ResetEngine()
    {
        // エンジン出力をリセット
        TopRight._enginePower = 0;
        TopLeft._enginePower = 0;
        BtmRight._enginePower = 0;
        BtmLeft._enginePower = 0;
        SideRight._enginePower = 0;
        SideLeft._enginePower = 0;

        //エンジンの向きをリセット
        TopRight.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        TopLeft.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        BtmRight.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        BtmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        SideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        SideLeft.transform.localRotation = quaternion.Euler(0f, 0f, 180f);
    }
}
