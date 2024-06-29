using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class DamageCtrl : MonoBehaviour
{
    [SerializeField] string _obstacleTagName = "Obstacle";
    [SerializeField] float _damageBase = 0.001f;
    [SerializeField] GameObject _animation = null;
    [Header("無敵時間")]
    [SerializeField] float _invincibilityDuration = 0.1f;
    [SerializeField] bool _invAnimation = true;
    [SerializeField] float _invFlashInterval = 0.1f;
    [SerializeField] Color _invAnimColor = new Color(1.0f, 0.8f, 0.75f);// オレンジ
    //プレイヤーの状態（Effect）
    private enum InvState
    {
        Normal,
        Invincibility,
    }
    [SerializeField] InvState _invState;

    [Header("プロパティ")]
    Rigidbody2D _rgd;
    SpriteRenderer _spr;
    HealthSystem _healthSystem;
    ShieldCtrl _shieldCtrl;
    Explosion _explosion;
    [SerializeField] float _invTimer;

    private void Start()
    {
        _rgd = this.GetComponent<Rigidbody2D>();
        _spr = this.GetComponent<SpriteRenderer>();
        _healthSystem = this.GetComponent<HealthSystem>();
        _shieldCtrl = this.GetComponent<ShieldCtrl>();
        _explosion = _animation.GetComponent<Explosion>();

        //コルーチンを開始
        if (_invAnimation == true)
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
                if (invFlag ==  false)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _obstacleTagName && _invState != InvState.Invincibility) //障害物にぶつかった && 無敵時間外
        {
            // スピードに応じて、ダメージを受ける
            float damage = _damageBase * _rgd.velocity.magnitude;
            _shieldCtrl.Damage(damage); //ヘルスからではなく、シールドからDamageを呼び出し

            _invTimer = _invincibilityDuration; //無敵時間 代入

            // アニメーションを再生
            Vector3 hitPos = collision.contacts[0].point;
            _explosion.Play(damage, hitPos);
        }
    }
}
