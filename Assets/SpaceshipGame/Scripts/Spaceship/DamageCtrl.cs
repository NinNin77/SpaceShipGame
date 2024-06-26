using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class DamageCtrl : MonoBehaviour
{
    [SerializeField] string _obstacleTagName = "Obstacle";
    [SerializeField] float _damageBase = 0.001f;
    [SerializeField] GameObject _animation = null;
    [Header("���G����")]
    [SerializeField] float _invincibilityDuration = 0.1f;
    [SerializeField] bool _invAnimation = true;
    [SerializeField] float _invFlashInterval = 0.1f;
    [SerializeField] Color _invAnimColor = new Color(1.0f, 0.8f, 0.75f);// �I�����W
    //�v���C���[�̏�ԁiEffect�j
    private enum InvState
    {
        Normal,
        Invincibility,
    }
    [SerializeField] InvState _invState;

    [Header("�v���p�e�B")]
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

        //�R���[�`�����J�n
        if (_invAnimation == true)
        {
            StartCoroutine(InvincibilityAnimation());
        }
    }
    private void Update()
    {
        //���G����
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

    IEnumerator InvincibilityAnimation() //���ꂩ��ʎY�������AEffect�n�̃e�X�g�Ƃ��Ă�����Ă�B
    {
        bool invFlag = false;
        Color orgColor = _spr.color; //���̐F��ۑ�

        while (true)
        {
            // ���G�A
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
                yield return new WaitForSeconds(_invFlashInterval); //����ł����̂��H���[�v�������x�����łȂ��Ȃ�A�x���Ȃ邾���ł́H
            }
            // �񖳓G�A
            else
            {
                // ���G��Ԃ̏I��莞�AEnd
                if (invFlag == true)
                {
                    invFlag = false;
                    _spr.enabled = true;
                    _spr.color = orgColor;
                }
                // �񖳓G��Ԃ̈ێ����A
                else
                {
                    orgColor = _spr.color; //���̐F��ۑ�
                }

                yield return null; //���̃t���[����҂�
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _obstacleTagName && _invState != InvState.Invincibility) //��Q���ɂԂ����� && ���G���ԊO
        {
            // �X�s�[�h�ɉ����āA�_���[�W���󂯂�
            float damage = _damageBase * _rgd.velocity.magnitude;
            _shieldCtrl.Damage(damage); //�w���X����ł͂Ȃ��A�V�[���h����Damage���Ăяo��

            _invTimer = _invincibilityDuration; //���G���� ���

            // �A�j���[�V�������Đ�
            Vector3 hitPos = collision.contacts[0].point;
            _explosion.Play(damage, hitPos);
        }
    }
}
