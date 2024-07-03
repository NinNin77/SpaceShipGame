using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldCtrl : MonoBehaviour
{
    /// <summary>
    /// 上下させたい時は、ModifyShield()を使う。
    /// </summary>
    [Header("シールド")]
    [SerializeField] public float _shield = 10.0f;
    [SerializeField] public float _maxShield = 10.0f;
    [SerializeField] public HealthSystem _healthSystem;

    private List<float> _timer = new List<float>() {0};

    /// <summary>
    /// シールド量を上下させる。
    /// </summary>
    /// <param name="amount">ダメージを与えたいと気は、マイナス値を入れる</param>
    /// <returns>返り値として余剰分を返す</returns>
    public float Modify(float amount)
    {
        float lastShield = _shield;
        float over = 0.0f;
        //まずはシールドを削る
        _shield += amount;

        //Modify後、maxを超える場合
        if (_shield > _maxShield)
        {
            over = _shield - _maxShield; //余りを保存
            _shield = _maxShield;
        }
        //Modify後、0未満になる場合
        else if (_shield < 0)
        {
            over = _shield; //余りを保存
            _shield = 0;
        }

        //返り値として余剰分を返す
        return over;

        //Log
        //Debug.Log($"Shield Modified. {lastShield} -> {_shield}");
    }

    void Update()
    {
        // 経過時間を加算
        for (int i = 0; i < _timer.Count; i++)
        {
            _timer[i] += Time.deltaTime;
        }

        // 0.2秒ごと
        if (_timer[0] >= 0.2f)
        {
            // 自動で回復
            _shield += 0.1f;
            if (_shield > _maxShield)
            {
                _shield = _maxShield;
            }

            // タイマーをリセット
            _timer[0] = 0.0f;
        }

        // interval1秒以上経過した場合
        //if (timer1 >= interval1)
        //{
        //    // タイマー1の処理を実行
        //    ChangeColor();
        //    // タイマーをリセット
        //    timer1 = 0f;
        //}
    }
}
