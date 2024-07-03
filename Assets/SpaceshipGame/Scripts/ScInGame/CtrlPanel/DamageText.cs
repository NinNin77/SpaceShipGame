using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class DamageText : MonoBehaviour
{
    [SerializeField] float _displaytime = 0.4f;
    [SerializeField] TMP_Text _tMP_Text; //使わない。形だけ。
    [Header("'UIヘルス&シールド'に表示する")]
    [SerializeField] bool _isDisplayHealthAndShield = true;

    bool _isInstant = false;
    float _timer;

    private void Awake()
    {
    }
    void Update()
    {
        if (_isInstant == true)
        {
            // タイマーで死ぬ
            _timer += Time.deltaTime;
            if (_timer > _displaytime)
                Destroy(gameObject);

            // 動く
            Vector3 pos = transform.localPosition;
            pos.y += 0.1f; //上に
            transform.localPosition = pos;
        }
    }

    public void DisplayDamage(float damage, GameObject target)
    {
        Transform trans;
        Vector2 pos;

        //モノの上に表示
        if (_isDisplayHealthAndShield == false)
        {
            trans = target.GetComponent<Transform>();
            pos = new Vector2(trans.position.x, trans.position.y + 1.0f); //表示位置
            InstantiateDamage(damage, pos);
        }
        //ヘルス表示の上に表示
        else
        {
            trans = this.GetComponent<Transform>();
            pos = new Vector2(trans.position.x, trans.position.y); //表示位置
            InstantiateDamage(damage, pos);
        }
    }
    void InstantiateDamage(float damage, Vector2 pos)
    {

        GameObject ins = Instantiate(this.gameObject, pos, transform.rotation, this.transform.parent); //インスタンスを生成
        TMP_Text text = ins.GetComponent<TMP_Text>();
        DamageText script = ins.GetComponent<DamageText>();

        //表示
        text.enabled = true;
        //インスタンスモードをOn
        script._isInstant = true;
        //ダメージ量を代入
        var tmp = damage * 10;
        text.SetText($"{tmp.ToString("###0.0")}");
    }
}
