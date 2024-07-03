using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    /// <summary>
    /// 上下させたい時は、Modify()を使う。
    /// </summary>
    [Header("ヘルス")]
    [SerializeField] public float _health = 10.0f;

    [Header("編集不要")]
    [Tooltip("trueの場合、Healthを元にMaxHealthが自動的に設定される。")]
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
    /// ヘルスを上下させたい時は、これを使う。
    /// </summary>
    /// <param name="amount">ダメージを与えたいと気は、マイナス値を入れる</param>
    public void Modify(float amount)
    {
        float lastHealth = _health;
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
            if (_destroy == true) { Destroy(this.gameObject); }//自身を破壊
        }

        //Log
        //Debug.Log($"Health Modified. {lastHealth} -> {_health}");
    }

    void Update()
    {
        //Modify後のヘルスが、maxHealthを超える場合
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
            Debug.LogWarning("関数[ModifyHealth]を介さず、[_health > _maxHealth]になった。" +
                "healthにmaxHealthを代入した。", this.gameObject);
        }
        //Modify後のヘルスが、0以下になる場合
        if (_health <= 0)
        {
            if (_destroy == true) 
            { 
            Destroy(this.gameObject);//自身を破壊
            Debug.LogWarning("関数[ModifyHealth]を介さず、[_health <= 0]になった。" +
            "オブジェクトは破壊した。", this.gameObject);
            }
        }
    }
}
