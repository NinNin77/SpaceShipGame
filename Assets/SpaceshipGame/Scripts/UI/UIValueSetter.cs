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
    [Header("詳細")]
    [SerializeField] public float _scale = 1.0f;

    /// <summary>値を適用/変更したいとき、外部からこの関数を呼び出せ。</summary>
    public void ValueChanged(float newValue)
    {
        // nullチェック
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

        // Valueの適用
        if (newValue < 0)
        {
            Debug.LogError("Negative values are not allowed.", this.gameObject);
            return;
        }
        _floatValue = newValue;

        // Targetに対して、変更。
        try //いろいろバグが起こりそうだからね
        {
            System.Type scriptType = _targetScript.GetType();
            FieldInfo fieldInfo = scriptType.GetField(_targetFieldName, BindingFlags.Instance | BindingFlags.Public);//privateフィールドのみ。publicはなし。
            PropertyInfo propertyInfo = scriptType.GetProperty(_targetFieldName, BindingFlags.Instance | BindingFlags.Public);

            if (fieldInfo != null) // フィールドの検索成功
            {
                fieldInfo.SetValue(_targetScript, _floatValue * _scale); //BUG: fieldInfoがfloat型じゃない
                Debug.Log($"Field '{_targetFieldName}' set to {_floatValue * _scale} in '{scriptType}'", this.gameObject);
            }
            else if (propertyInfo != null) // プロパティの検索成功
            {
                propertyInfo.SetValue(_targetScript, _floatValue * _scale); //BUG: float型じゃない
                Debug.Log($"Property '{_targetFieldName}' set to {_floatValue * _scale} in '{scriptType}'", this.gameObject);
            }
            else // 失敗
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
