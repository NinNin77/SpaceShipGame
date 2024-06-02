using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CtrlInput : MonoBehaviour
{
    // 返り値
    public InputDirection inputDirection = InputDirection.None;
    public float inputMainEngineAddPower = 0f;
    public bool inputMainEngineIsOn = false;

    /// <summary>
    /// デバイスからの入力を取得.上下左右の方向
    /// </summary>
    public enum InputDirection
    {
        None = 0,
        Front,
        Back,
        Right,
        Left
    }

    // 定義
    Dictionary<KeyCode, float> wasdPressTime = new Dictionary<KeyCode, float>();
    float shiftPressTime = 0f;

    void Start()
    {
        // 初期化
        wasdPressTime[KeyCode.W] = 0;
        wasdPressTime[KeyCode.A] = 0;
        wasdPressTime[KeyCode.S] = 0;
        wasdPressTime[KeyCode.D] = 0;
    }

    void Update()
    {
        inputDirection = WASD_NoOverlap();
        ShiftandScroll();
    }

    /// <summary>
    /// WASDを取得
    /// キーかぶりが無いようにします
    /// </summary>
    InputDirection WASD_NoOverlap()
    {
        //　キーの長押し時間を取得
        var tmpKeys = new Dictionary<KeyCode, float>(wasdPressTime);
        foreach (KeyCode keycode in tmpKeys.Keys)
        {
            if (Input.GetKey(keycode))
            {
                wasdPressTime[keycode] += Time.deltaTime;
            }
            else
            {
                wasdPressTime[keycode] = 0.0f;
            }
        }

        // キー長押し時間を比べて、最も小さいものを残す
        KeyCode tmpKey = KeyCode.None;
        float tmpTime = float.MaxValue;
        foreach (var pair in wasdPressTime)
        {
            if (pair.Value > 0 && pair.Value < tmpTime)
            {
                tmpKey = pair.Key;
                tmpTime = pair.Value;
            }
        }

        // 返り値
        switch (tmpKey)
        {
            case KeyCode.W: return InputDirection.Front;
            case KeyCode.A: return InputDirection.Left;
            case KeyCode.S: return InputDirection.Back;
            case KeyCode.D: return InputDirection.Right;
            default: return InputDirection.None;
        }
    }
    
    /// <summary>
    /// LeftShiftを押してる場合、
    /// マウスホイールのを取得
    /// </summary>
    void ShiftandScroll()
    {
        // 定義/初期化
        float wh = 0;

        //　キーの長押し時間を取得/リセット
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftPressTime += Time.deltaTime;
        }
        else
        {
            shiftPressTime = 0.0f;
        }

        //　Shiftが押されてる場合、
        if (shiftPressTime > 0)
        {
            //マウスホイールを取得
            wh = Input.GetAxis("Mouse ScrollWheel");
        }


        //返り値
        if (wh != 0)
        {
            inputMainEngineAddPower += wh;　//マウスホイール
        }

        if (shiftPressTime > 0)
        {
            inputMainEngineIsOn = true; //シフトが押されている時
        }
        else
        {
            inputMainEngineIsOn = false; //シフトが押されていない時
        }
        
    }
}

