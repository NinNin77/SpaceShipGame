using System.Collections.Generic;
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
    /// <summary>即死を防止する機能。どんな大きいdamageも必ず一回はShieldが吸収する。</summary>
    [SerializeField] public bool _preventInstantDeath = true;
    /// <summary>PreventInstantDeathが発動するShield量。</summary>
    [SerializeField] public float _PIDShieldAmount = 9.0f;

    private List<float> _timer = new List<float>() {0};

    /// <summary>
    /// 'ModifyHealth'の代わりに、これを使う。
    /// </summary>
    /// <param name="damage">ダメージ量。プラスの値を入れてね。</param>
    public void Damage(float damage)
    {
        float lastShield = _shield;
        float healthDamage = 0.0f; //プラス値
        //まずはシールドを削る
        _shield -= damage;

        //シールドが、0未満になる場合
        if (_shield < 0)
        {
            // 即死防止機能が発動
            if (_preventInstantDeath == true && lastShield　>= _PIDShieldAmount)
            {
                _shield = 0;
            }
            //発動しない
            else
            {
                healthDamage = -_shield; //プラスに治す
                _shield = 0;
            }
                
        }
        //ヘルスを削る
        if (healthDamage > 0) { }
        {
            _healthSystem.ModifyHealth(-healthDamage);
        }

        //Log
        Debug.Log($"SpaceShip had {damage} damage", this.gameObject);
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
