using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCtrl : MonoBehaviour
{
    // �C���X�y�N�^�[
    [SerializeField] string _obstacleTagName = "Obstacle";
    [SerializeField] float _damageBase = 0.001f;
    [SerializeField] GameObject _animation = null;
    [SerializeField] bool _isDamageText = true;
    [SerializeField] DamageText _damageText = null;
    [Header("������h�~����V�[���h")]
    /// <summary>������h�~����@�\�B�ǂ�ȑ傫��damage���K������Shield���z������B</summary>
    [SerializeField] public bool _isPreventInstantDeath = true;
    /// <summary>PreventInstantDeath����������Shield�ʁB</summary>
    [SerializeField] public float _PIDShieldAmount = 9.0f;
    [Header("���G����")]
    [SerializeField] float _invincibilityDuration = 0.1f;
    [SerializeField] bool _isInvAnimation = true;
    [SerializeField] float _invFlashInterval = 0.1f;
    [SerializeField] Color _invAnimColor = new Color(1.0f, 0.8f, 0.75f);// �I�����W
    private enum InvState
    {
        Normal,
        Invincibility,
    }�@//�v���C���[�̏�ԁiEffect�j
    [SerializeField] InvState _invState;

    // �v���p�e�B
    Rigidbody2D _rgd;
    SpriteRenderer _spr;
    HealthSystem _healthSystem;
    ShieldCtrl _shieldCtrl;
    Explosion _explosion;
    [SerializeField] float _invTimer;
    private Vector2 _previousVel; //�O�̑��x

    private void Start()
    {
        _rgd = this.GetComponent<Rigidbody2D>();
        _spr = this.GetComponent<SpriteRenderer>();
        _healthSystem = this.GetComponent<HealthSystem>();
        _shieldCtrl = this.GetComponent<ShieldCtrl>();
        _explosion = _animation.GetComponent<Explosion>();

        //�R���[�`�����J�n
        if (_isInvAnimation == true)
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

    /// <summary>
    /// 'Modify'�̑���ɁA������g���B
    /// </summary>
    /// <param name="damage">�_���[�W�ʁB�v���X�̒l�����ĂˁB</param>
    public void Damage(float damage)
    {
        float lastShield = _shieldCtrl._shield;

        //�܂��̓V�[���h�����
        float over = _shieldCtrl.Modify(-damage);

        //�}�C�i�X�̗]�肪���������ꍇ
        if (over < 0)
        {
            // �����h�~�@�\������
            if (_isPreventInstantDeath == true && lastShield >= _PIDShieldAmount)
            {
                //�Ȃɂ����Ȃ�
            }
            //�������Ȃ�
            else
            {
                //�w���X�����
               _healthSystem.Modify(over); //over�̓}�C�i�X�̒l�Ȃ̂ł��̂܂ܑ��
            }
        }

        //Text��\��
        if (_isDamageText == true)
        {
            _damageText.DisplayDamage(damage, this.gameObject);
        }

        //Log
        Debug.Log($"SpaceShip had {damage} damage", this.gameObject);
    }

    private void FixedUpdate()
    {
        // ���t���[���̑��x��ۑ�
        _previousVel = _rgd.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _obstacleTagName && _invState != InvState.Invincibility) //��Q���ɂԂ����� && ���G���ԊO
        {
            // ���Α��x�̌v�Z
            Rigidbody2D otherRgd = collision.rigidbody;// �ՓˑΏۂ̃��W�b�h�{�f�B
            //�O�̑��x�ƁA�ՓˑΏۂ̑��x�ŁA���Α��x�����߂�B
            Vector2 relativeVel = _previousVel - otherRgd.velocity; 

            // �ՓˑO�̑��x���g�p���āA�_���[�W���v�Z
            float damage = _damageBase * relativeVel.magnitude;
            Damage(damage); //Damage���Ăяo��

            _invTimer = _invincibilityDuration; //���G���� ���

            // �A�j���[�V�������Đ�
            Vector3 hitPos = collision.contacts[0].point;
            _explosion.Play(damage, hitPos);
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
}
