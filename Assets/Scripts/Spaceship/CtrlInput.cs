using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CtrlInput : MonoBehaviour
{
    // �Ԃ�l
    public InputDirection inputDirection = InputDirection.None;
    public float inputMainEngineAddPower = 0f;
    public bool inputMainEngineIsOn = false;

    /// <summary>
    /// �f�o�C�X����̓��͂��擾.�㉺���E�̕���
    /// </summary>
    public enum InputDirection
    {
        None = 0,
        Front,
        Back,
        Right,
        Left
    }

    // ��`
    Dictionary<KeyCode, float> wasdPressTime = new Dictionary<KeyCode, float>();
    float shiftPressTime = 0f;

    void Start()
    {
        // ������
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
    /// WASD���擾
    /// �L�[���Ԃ肪�����悤�ɂ��܂�
    /// </summary>
    InputDirection WASD_NoOverlap()
    {
        //�@�L�[�̒��������Ԃ��擾
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

        // �L�[���������Ԃ��ׂāA�ł����������̂��c��
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

        // �Ԃ�l
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
    /// LeftShift�������Ă�ꍇ�A
    /// �}�E�X�z�C�[���̂��擾
    /// </summary>
    void ShiftandScroll()
    {
        // ��`/������
        float wh = 0;

        //�@�L�[�̒��������Ԃ��擾/���Z�b�g
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftPressTime += Time.deltaTime;
        }
        else
        {
            shiftPressTime = 0.0f;
        }

        //�@Shift��������Ă�ꍇ�A
        if (shiftPressTime > 0)
        {
            //�}�E�X�z�C�[�����擾
            wh = Input.GetAxis("Mouse ScrollWheel");
        }


        //�Ԃ�l
        if (wh != 0)
        {
            inputMainEngineAddPower += wh;�@//�}�E�X�z�C�[��
        }

        if (shiftPressTime > 0)
        {
            inputMainEngineIsOn = true; //�V�t�g��������Ă��鎞
        }
        else
        {
            inputMainEngineIsOn = false; //�V�t�g��������Ă��Ȃ���
        }
        
    }
}

