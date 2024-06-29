using System.Collections.Generic;
using UnityEngine;

public class ShieldCtrl : MonoBehaviour
{
    /// <summary>
    /// �㉺�����������́AModifyShield()���g���B
    /// </summary>
    [Header("�V�[���h")]
    [SerializeField] public float _shield = 10.0f;
    [SerializeField] public float _maxShield = 10.0f;
    [SerializeField] public HealthSystem _healthSystem;
    /// <summary>������h�~����@�\�B�ǂ�ȑ傫��damage���K������Shield���z������B</summary>
    [SerializeField] public bool _preventInstantDeath = true;
    /// <summary>PreventInstantDeath����������Shield�ʁB</summary>
    [SerializeField] public float _PIDShieldAmount = 9.0f;

    private List<float> _timer = new List<float>() {0};

    /// <summary>
    /// 'ModifyHealth'�̑���ɁA������g���B
    /// </summary>
    /// <param name="damage">�_���[�W�ʁB�v���X�̒l�����ĂˁB</param>
    public void Damage(float damage)
    {
        float lastShield = _shield;
        float healthDamage = 0.0f; //�v���X�l
        //�܂��̓V�[���h�����
        _shield -= damage;

        //�V�[���h���A0�����ɂȂ�ꍇ
        if (_shield < 0)
        {
            // �����h�~�@�\������
            if (_preventInstantDeath == true && lastShield�@>= _PIDShieldAmount)
            {
                _shield = 0;
            }
            //�������Ȃ�
            else
            {
                healthDamage = -_shield; //�v���X�Ɏ���
                _shield = 0;
            }
                
        }
        //�w���X�����
        if (healthDamage > 0) { }
        {
            _healthSystem.ModifyHealth(-healthDamage);
        }

        //Log
        Debug.Log($"SpaceShip had {damage} damage", this.gameObject);
    }

    void Update()
    {
        // �o�ߎ��Ԃ����Z
        for (int i = 0; i < _timer.Count; i++)
        {
            _timer[i] += Time.deltaTime;
        }

        // 0.2�b����
        if (_timer[0] >= 0.2f)
        {
            // �����ŉ�
            _shield += 0.1f;
            if (_shield > _maxShield)
            {
                _shield = _maxShield;
            }

            // �^�C�}�[�����Z�b�g
            _timer[0] = 0.0f;
        }

        // interval1�b�ȏ�o�߂����ꍇ
        //if (timer1 >= interval1)
        //{
        //    // �^�C�}�[1�̏��������s
        //    ChangeColor();
        //    // �^�C�}�[�����Z�b�g
        //    timer1 = 0f;
        //}
    }
}
