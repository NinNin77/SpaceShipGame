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
    [SerializeField] public float power = 0f;
    [SerializeField] public float maxPower = 1.0f; //使ってない

    // アサイン　
    CoreEngines CoreEngines;
    //各エンジンを登録
    TopRight TopRight;
    TopLeft TopLeft;
    BtmRight BtmRight;
    BtmLeft BtmLeft;
    SideRight SideRight;
    SideLeft SideLeft;

    private void Start()
    {
        // 初期化
        CoreEngines = GetComponentInParent<CoreEngines>();
        TopRight = GetComponentInChildren<TopRight>();
        TopLeft = GetComponentInChildren<TopLeft>();
        BtmRight = GetComponentInChildren<BtmRight>();
        BtmLeft = GetComponentInChildren<BtmLeft>();
        SideRight = GetComponentInChildren<SideRight>();
        SideLeft = GetComponentInChildren<SideLeft>();
    }

    public void Effect_Forward()
    {
        Effect_ResetEngine();// リセット

        // エンジンの出力
        float myFloat = CoreEngines.SubEngine_Particle;
        BtmRight.enginePower = myFloat / 4;
        BtmLeft.enginePower = myFloat / 4;
        SideRight.enginePower = myFloat / 4;
        SideLeft.enginePower = myFloat / 4;

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
        float myFloat = CoreEngines.SubEngine_Particle;
        TopRight.enginePower = myFloat / 4;
        TopLeft.enginePower = myFloat / 4;
        SideRight.enginePower = myFloat / 4;
        SideLeft.enginePower = myFloat / 4;

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
        float myFloat = CoreEngines.SubEngine_Particle;
        TopLeft.enginePower = myFloat / 4;
        BtmRight.enginePower = myFloat / 4;
        SideRight.enginePower = myFloat / 4;
        SideLeft.enginePower = myFloat / 4;

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
        float myFloat = CoreEngines.SubEngine_Particle;
        TopRight.enginePower = myFloat / 4;
        BtmLeft.enginePower = myFloat / 4;
        SideRight.enginePower = myFloat / 4;
        SideLeft.enginePower = myFloat / 4;

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
        TopRight.enginePower = 0;
        TopLeft.enginePower = 0;
        BtmRight.enginePower = 0;
        BtmLeft.enginePower = 0;
        SideRight.enginePower = 0;
        SideLeft.enginePower = 0;

        //エンジンの向きをリセット
        TopRight.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        TopLeft.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        BtmRight.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        BtmLeft.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        SideRight.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        SideLeft.transform.localRotation = quaternion.Euler(0f, 0f, 180f);
    }
}
