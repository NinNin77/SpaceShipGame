using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCtrl : MonoBehaviour
{
    [SerializeField] string _obstacleTagName = "Obstacle";
    [SerializeField] float _damageBase = 0.001f;
    [Header("���G����")]
    [SerializeField] float _invincibilityDuration = 0.1f;
    [SerializeField] bool _invAnimation = true;
    [SerializeField] float _invFlashInterval = 0.1f;
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
    [SerializeField] float _invTimer;

    private void Start()
    {
        _rgd = this.GetComponent<Rigidbody2D>();
        _spr = this.GetComponent<SpriteRenderer>();
        _healthSystem = this.GetComponent<HealthSystem>();

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

    IEnumerator InvincibilityAnimation()
    {
        while (true)
        {
            if (_invState == InvState.Invincibility)
            {
                _spr.enabled = !_spr.enabled;//���]������B
                yield return new WaitForSeconds(_invFlashInterval);
            }
            else
            {
                _spr.enabled = true; // ���G��Ԃ��I�������X�v���C�g��\��
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
            Debug.Log($"SpaceShip had {damage} damage", this.gameObject);
            _healthSystem.ModifyHealth(-damage);

            _invTimer = _invincibilityDuration; //���G���� ���
        }
    }
}
