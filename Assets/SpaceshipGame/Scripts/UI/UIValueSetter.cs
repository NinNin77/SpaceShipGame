using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class UIValueSetter : MonoBehaviour
{
    [SerializeField] private float _floatValue;

    [SerializeField] public MonoBehaviour _targetScript;
    [SerializeField] public string _targetFieldName;
    [Header("�ڍ�")]
    [SerializeField] public float _scale = 1.0f;

    /// <summary>�l��K�p/�ύX�������Ƃ��A�O�����炱�̊֐����Ăяo���B</summary>
    public void ValueChanged(float newValue)
    {
        // null�`�F�b�N
        if (_targetScript == null)
        {
            Debug.LogError($"'{_targetScript}' is null.", this.gameObject);
            return;
        }

        if (string.IsNullOrEmpty(_targetFieldName))
        {
            Debug.LogError($"'{_targetFieldName}' is null.", this.gameObject);
            return;
        }

        // Value�̓K�p
        if (newValue < 0)
        {
            Debug.LogError("Negative values are not allowed.", this.gameObject);
            return;
        }
        _floatValue = newValue;

        // Target�ɑ΂��āA�ύX�B
        try //���낢��o�O���N���肻���������
        {
            System.Type scriptType = _targetScript.GetType();
            FieldInfo fieldInfo = scriptType.GetField(_targetFieldName, BindingFlags.Instance | BindingFlags.Public);//private�t�B�[���h�̂݁Bpublic�͂Ȃ��B
            PropertyInfo propertyInfo = scriptType.GetProperty(_targetFieldName, BindingFlags.Instance | BindingFlags.Public);

            if (fieldInfo != null) // �t�B�[���h�̌�������
            {
                fieldInfo.SetValue(_targetScript, _floatValue * _scale); //BUG: fieldInfo��float�^����Ȃ�
                Debug.Log($"Field '{_targetFieldName}' set to {_floatValue * _scale} in '{scriptType}'", this.gameObject);
            }
            else if (propertyInfo != null) // �v���p�e�B�̌�������
            {
                propertyInfo.SetValue(_targetScript, _floatValue * _scale); //BUG: float�^����Ȃ�
                Debug.Log($"Property '{_targetFieldName}' set to {_floatValue * _scale} in '{scriptType}'", this.gameObject);
            }
            else // ���s
            {
                Debug.LogError($"Field or Property'{_targetFieldName}' not found in '{scriptType}'", this.gameObject);
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
        }
    }
}
