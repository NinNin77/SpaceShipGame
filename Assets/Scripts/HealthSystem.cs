using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    /// <summary>
    /// �㉺�����������́AModifyHealth()���g���B
    /// </summary>
    [Header("�w���X")]
    [SerializeField] public float _health = 10.0f;

    [Header("�ҏW�s�v")]
    [Tooltip("true�̏ꍇ�AHealth������MaxHealth�������I�ɐݒ肳���B")]
    [SerializeField] public bool _autoMaxHealth = true;
    [SerializeField] public float _maxHealth = 10.0f;

    void Start()
    {
        if (_autoMaxHealth == true)
        {
            _maxHealth = _health;
        }
    }

    /// <summary>
    /// �w���X���㉺�����������́A������g���B
    /// </summary>
    /// <param name="amount"></param>
    public void ModifyHealth(float amount)
    {
        //Modify
        _health += amount;

        //Modify��̃w���X���AmaxHealth�𒴂���ꍇ
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        //Modify��̃w���X���A0�����ɂȂ�ꍇ
        if (_health < 0)
        {
            _health = 0;
        }

        //DEAD
        if (_health <= 0)
        {
            Destroy(this.gameObject);//���g��j��
        }
    }

    void Update()
    {
        //Modify��̃w���X���AmaxHealth�𒴂���ꍇ
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
            Debug.Log("�֐�[ModifyHealth]������A[health > maxHealth]�ɂȂ����B" +
                "health��maxHealth���������B", this.gameObject);
        }
        //Modify��̃w���X���A0�����ɂȂ�ꍇ
        if (_health < 0)
        {
            Destroy(this.gameObject);//���g��j��
            Debug.Log("�֐�[ModifyHealth]������A[health < 0]�ɂȂ����B" +
                "�I�u�W�F�N�g�͔j�󂵂��B", this.gameObject);
        }
    }
}
