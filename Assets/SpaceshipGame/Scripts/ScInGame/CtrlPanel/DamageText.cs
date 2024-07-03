using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class DamageText : MonoBehaviour
{
    [SerializeField] float _displaytime = 0.4f;
    [SerializeField] TMP_Text _tMP_Text; //�g��Ȃ��B�`�����B
    [Header("'UI�w���X&�V�[���h'�ɕ\������")]
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
            // �^�C�}�[�Ŏ���
            _timer += Time.deltaTime;
            if (_timer > _displaytime)
                Destroy(gameObject);

            // ����
            Vector3 pos = transform.localPosition;
            pos.y += 0.1f; //���
            transform.localPosition = pos;
        }
    }

    public void DisplayDamage(float damage, GameObject target)
    {
        Transform trans;
        Vector2 pos;

        //���m�̏�ɕ\��
        if (_isDisplayHealthAndShield == false)
        {
            trans = target.GetComponent<Transform>();
            pos = new Vector2(trans.position.x, trans.position.y + 1.0f); //�\���ʒu
            InstantiateDamage(damage, pos);
        }
        //�w���X�\���̏�ɕ\��
        else
        {
            trans = this.GetComponent<Transform>();
            pos = new Vector2(trans.position.x, trans.position.y); //�\���ʒu
            InstantiateDamage(damage, pos);
        }
    }
    void InstantiateDamage(float damage, Vector2 pos)
    {

        GameObject ins = Instantiate(this.gameObject, pos, transform.rotation, this.transform.parent); //�C���X�^���X�𐶐�
        TMP_Text text = ins.GetComponent<TMP_Text>();
        DamageText script = ins.GetComponent<DamageText>();

        //�\��
        text.enabled = true;
        //�C���X�^���X���[�h��On
        script._isInstant = true;
        //�_���[�W�ʂ���
        var tmp = damage * 10;
        text.SetText($"{tmp.ToString("###0.0")}");
    }
}
