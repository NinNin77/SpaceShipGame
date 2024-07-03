using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCtrl : MonoBehaviour
{
    // インスペクター
    [SerializeField] string _obstacleTagName = "Obstacle";
    [SerializeField] float _damageBase = 0.001f;
    [SerializeField] GameObject _animation = null;
    [SerializeField] bool _isDamageText = true;
    [SerializeField] DamageText _damageText = null;
    [Header("即死を防止するシールド")]
    /// <summary>即死を防止する機能。どんな大きいdamageも必ず一回はShieldが吸収する。</summary>
    [SerializeField] public bool _isPreventInstantDeath = true;
    /// <summary>PreventInstantDeathが発動するShield量。</summary>
    [SerializeField] public float _PIDShieldAmount = 9.0f;
    [Header("無敵時間")]
    [SerializeField] float _invincibilityDuration = 0.1f;
    [SerializeField] bool _isInvAnimation = true;
    [SerializeField] float _invFlashInterval = 0.1f;
    [SerializeField] Color _invAnimColor = new Color(1.0f, 0.8f, 0.75f);// オレンジ
    private enum InvState
    {
        Normal,
        Invincibility,
    }　//プレイヤーの状態（Effect）
    [SerializeField] InvState _invState;

    // プロパティ
    Rigidbody2D _rgd;
    SpriteRenderer _spr;
    HealthSystem _healthSystem;
    ShieldCtrl _shieldCtrl;
    Explosion _explosion;
    [SerializeField] float _invTimer;
    private Vector2 _previousVel; //前の速度

    private void Start()
    {
        _rgd = this.GetComponent<Rigidbody2D>();
        _spr = this.GetComponent<SpriteRenderer>();
        _healthSystem = this.GetComponent<HealthSystem>();
        _shieldCtrl = this.GetComponent<ShieldCtrl>();
        _explosion = _animation.GetComponent<Explosion>();

        //コルーチンを開始
        if (_isInvAnimation == true)
        {
            StartCoroutine(InvincibilityAnimation());
        }
    }
    private void Update()
    {
        //無敵時間
        if (_invTimer > 0)
        {
            _invTimer -= Time.deltaTime;
            _invState = InvState.Invincibility;
        }
        else
        {
            _invTimer = 0;
            _invState = InvState.Normal;
        }
    }

    /// <summary>
    /// 'Modify'の代わりに、これを使う。
    /// </summary>
    /// <param name="damage">ダメージ量。プラスの値を入れてね。</param>
    public void Damage(float damage)
    {
        float lastShield = _shieldCtrl._shield;

        //まずはシールドを削る
        float over = _shieldCtrl.Modify(-damage);

        //マイナスの余りが発生した場合
        if (over < 0)
        {
            // 即死防止機能が発動
            if (_isPreventInstantDeath == true && lastShield >= _PIDShieldAmount)
            {
                //なにもしない
            }
            //発動しない
            else
            {
                //ヘルスを削る
               _healthSystem.Modify(over); //overはマイナスの値なのでそのまま代入
            }
        }

        //Textを表示
        if (_isDamageText == true)
        {
            _damageText.DisplayDamage(damage, this.gameObject);
        }

        //Log
        Debug.Log($"SpaceShip had {damage} damage", this.gameObject);
    }

    private void FixedUpdate()
    {
        // 毎フレームの速度を保存
        _previousVel = _rgd.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _obstacleTagName && _invState != InvState.Invincibility) //障害物にぶつかった && 無敵時間外
        {
            // 相対速度の計算
            Rigidbody2D otherRgd = collision.rigidbody;// 衝突対象のリジッドボディ
            //前の速度と、衝突対象の速度で、相対速度を求める。
            Vector2 relativeVel = _previousVel - otherRgd.velocity; 

            // 衝突前の速度を使用して、ダメージを計算
            float damage = _damageBase * relativeVel.magnitude;
            Damage(damage); //Damageを呼び出し

            _invTimer = _invincibilityDuration; //無敵時間 代入

            // アニメーションを再生
            Vector3 hitPos = collision.contacts[0].point;
            _explosion.Play(damage, hitPos);
        }
    }

    IEnumerator InvincibilityAnimation() //これから量産したい、Effect系のテストとしても作ってる。
    {
        bool invFlag = false;
        Color orgColor = _spr.color; //元の色を保存

        while (true)
        {
            // 無敵、
            if (_invState == InvState.Invincibility)
            {
                // Begin
                if (invFlag == false)
                {
                    invFlag = true;
                    _spr.color = _invAnimColor;
                }
                // loop
                else if (_spr.color == Color.white) //white
                {
                    //_spr.enabled = !_spr.enabled;
                    //_spr.color = Color.red;
                    _spr.color = _invAnimColor;
                }
                else
                {
                    _spr.color = Color.white;
                }
                yield return new WaitForSeconds(_invFlashInterval); //これでいいのか？ループ処理速度が一定でなくなる、遅くなるだけでは？
            }
            // 非無敵、
            else
            {
                // 無敵状態の終わり時、End
                if (invFlag == true)
                {
                    invFlag = false;
                    _spr.enabled = true;
                    _spr.color = orgColor;
                }
                // 非無敵状態の維持時、
                else
                {
                    orgColor = _spr.color; //元の色を保存
                }

                yield return null; //次のフレームを待つ
            }
        }
    }
}
