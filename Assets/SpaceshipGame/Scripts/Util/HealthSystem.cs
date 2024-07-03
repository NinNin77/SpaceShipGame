using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    /// <summary>
    /// �㉺�����������́AModify()���g���B
    /// </summary>
    [Header("�w���X")]
    [SerializeField] public float _health = 10.0f;

    [Header("�ҏW�s�v")]
    [Tooltip("true�̏ꍇ�AHealth������MaxHealth�������I�ɐݒ肳���B")]
    [SerializeField] public bool _autoMaxHealth = true;
    [SerializeField] public float _maxHealth = 10.0f;
    [SerializeField] public bool _destroy = true;

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
    /// <param name="amount">�_���[�W��^�������ƋC�́A�}�C�i�X�l������</param>
    public void Modify(float amount)
    {
        float lastHealth = _health;
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
            if (_destroy == true) { Destroy(this.gameObject); }//���g��j��
        }

        //Log
        //Debug.Log($"Health Modified. {lastHealth} -> {_health}");
    }

    void Update()
    {
        //Modify��̃w���X���AmaxHealth�𒴂���ꍇ
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
            Debug.LogWarning("�֐�[ModifyHealth]������A[_health > _maxHealth]�ɂȂ����B" +
                "health��maxHealth���������B", this.gameObject);
        }
        //Modify��̃w���X���A0�ȉ��ɂȂ�ꍇ
        if (_health <= 0)
        {
            if (_destroy == true) 
            { 
            Destroy(this.gameObject);//���g��j��
            Debug.LogWarning("�֐�[ModifyHealth]������A[_health <= 0]�ɂȂ����B" +
            "�I�u�W�F�N�g�͔j�󂵂��B", this.gameObject);
            }
        }
    }
}
