using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    /// <summary>
    /// 上下させたい時は、ModifyHealth()を使う。
    /// </summary>
    [Header("ヘルス")]
    [SerializeField] public float _health = 10.0f;

    [Header("編集不要")]
    [Tooltip("trueの場合、Healthを元にMaxHealthが自動的に設定される。")]
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
    /// ヘルスを上下させたい時は、これを使う。
    /// </summary>
    /// <param name="amount"></param>
    public void ModifyHealth(float amount)
    {
        //Modify
        _health += amount;

        //Modify後のヘルスが、maxHealthを超える場合
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        //Modify後のヘルスが、0未満になる場合
        if (_health < 0)
        {
            _health = 0;
        }

        //DEAD
        if (_health <= 0)
        {
            Destroy(this.gameObject);//自身を破壊
        }
    }

    void Update()
    {
        //Modify後のヘルスが、maxHealthを超える場合
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
            Debug.Log("関数[ModifyHealth]を介さず、[health > maxHealth]になった。" +
                "healthにmaxHealthを代入した。", this.gameObject);
        }
        //Modify後のヘルスが、0未満になる場合
        if (_health < 0)
        {
            Destroy(this.gameObject);//自身を破壊
            Debug.Log("関数[ModifyHealth]を介さず、[health < 0]になった。" +
                "オブジェクトは破壊した。", this.gameObject);
        }
    }
}
