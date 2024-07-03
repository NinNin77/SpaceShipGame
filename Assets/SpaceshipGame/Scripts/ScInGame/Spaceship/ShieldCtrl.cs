using System.Collections.Generic;
using Unity.VisualScripting;
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

    private List<float> _timer = new List<float>() {0};

    /// <summary>
    /// �V�[���h�ʂ��㉺������B
    /// </summary>
    /// <param name="amount">�_���[�W��^�������ƋC�́A�}�C�i�X�l������</param>
    /// <returns>�Ԃ�l�Ƃ��ė]�蕪��Ԃ�</returns>
    public float Modify(float amount)
    {
        float lastShield = _shield;
        float over = 0.0f;
        //�܂��̓V�[���h�����
        _shield += amount;

        //Modify��Amax�𒴂���ꍇ
        if (_shield > _maxShield)
        {
            over = _shield - _maxShield; //�]���ۑ�
            _shield = _maxShield;
        }
        //Modify��A0�����ɂȂ�ꍇ
        else if (_shield < 0)
        {
            over = _shield; //�]���ۑ�
            _shield = 0;
        }

        //�Ԃ�l�Ƃ��ė]�蕪��Ԃ�
        return over;

        //Log
        //Debug.Log($"Shield Modified. {lastShield} -> {_shield}");
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
