using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCtrl : MonoBehaviour
{
    [SerializeField] string _obstacleTagName = "Obstacle";
    [SerializeField] float _damageBase = 0.001f;
    [Header("無敵時間")]
    [SerializeField] float _invincibilityDuration = 0.1f;
    [SerializeField] bool _invAnimation = true;
    [SerializeField] float _invFlashInterval = 0.1f;
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
    [SerializeField] float _invTimer;

    private void Start()
    {
        _rgd = this.GetComponent<Rigidbody2D>();
        _spr = this.GetComponent<SpriteRenderer>();
        _healthSystem = this.GetComponent<HealthSystem>();

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

    IEnumerator InvincibilityAnimation()
    {
        while (true)
        {
            if (_invState == InvState.Invincibility)
            {
                _spr.enabled = !_spr.enabled;//反転させる。
                yield return new WaitForSeconds(_invFlashInterval);
            }
            else
            {
                _spr.enabled = true; // 無敵状態が終わったらスプライトを表示
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
            Debug.Log($"SpaceShip had {damage} damage", this.gameObject);
            _healthSystem.ModifyHealth(-damage);

            _invTimer = _invincibilityDuration; //無敵時間 代入
        }
    }
}
