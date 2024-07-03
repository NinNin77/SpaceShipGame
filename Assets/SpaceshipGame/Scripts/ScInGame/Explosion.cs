using System;
using System.ComponentModel;
using System.Drawing;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [Header("Animator")]
    [SerializeField] private float _animScale = 1.5f;
    [SerializeField] private float _sizeWithoutAnim = 0.5f;
    [Header("Particle")]
    [SerializeField] private GameObject _objParticle;
    [SerializeField] private float _parScale = 1.5f;
    [Tooltip("���C�t�^�C��")]
    [SerializeField] bool _mainLife = true;
    [SerializeField] private float _mainLifeMin = 0.1f;
    [SerializeField] private float _mainLifeMax = 0.4f;
    [Tooltip("���x")]
    [SerializeField] bool _mainSpeed = true;
    [SerializeField] private float _mainSpeedMin = 0.5f;
    [SerializeField] private float _mainSpeedMax = 5f;
    [Tooltip("�傫��")]
    [SerializeField] bool _mainSize = true;
    [SerializeField] private float _mainSizeMin = 0.005f;
    [SerializeField] private float _mainSizeMax = 0.05f;
    [Tooltip("��")]
    [SerializeField] bool _burst = true;
    [SerializeField] private float _burstMinCount = 15;
    [SerializeField] private float _burstMaxCount = 25;
    [Header("Audio")]
    [SerializeField] private GameObject _objAudio;
    [SerializeField] private float _audioVolume = 1.0f;
    [SerializeField] private float _audioPitch = 1.5f;

    private void Start()
    {
        if (_parent == null)// �s����ōČ����̂Ȃ��Ӗ��s���G���[
        {
            Debug.LogWarning("Start: _parent is null", gameObject);
        }
    }

    /// <summary>�C���X�^���X�𐶐����A�A�j���[�V�������Đ�����</summary>
    public void Play(float size, Vector3 pos)
    {
        Vector3 orgScale = this.transform.localScale;

        // �C���X�^���X ����
        if (_parent == null)
        {
            Debug.LogWarning("_parent is null",gameObject);
            _parent = GameObject.Find("ExplosionEffects");//_parent ���Đݒ� *�B��o�O���������H����
        }
        GameObject ins = Instantiate(this.gameObject, pos, transform.rotation, _parent.transform);

        // On�ɂ���
        ins.SetActive(true);

        // �A�j���[�V����
        Animator animator = ins.GetComponent<Animator>();
        if (size < _sizeWithoutAnim) //�ŏ�siza�ȉ���������A�A�j���[�V������\�����Ȃ�
        {
            animator.enabled = false;
        }
        ins.transform.localScale = new Vector3(orgScale[0] * size * _animScale, orgScale[1] * size * _animScale, orgScale[2]); //�X�P�[���𒼐ڂ�����

        // ��
        Audio(size, pos);

        // �p�[�e�B�N��
        Particle(size, pos);
    }

    void Particle(float size, Vector3 pos)
    {
        GameObject insPar = Instantiate(_objParticle, pos, transform.rotation, _parent.transform); //�C���X�^���X����
        //�ϐ�
        ParticleSystem insPS = insPar.GetComponent<ParticleSystem>();
        var insPSmain = insPS.main;

        //�e��ݒ�
        //���C�t�^�C��
        if (_mainLife == true) { insPSmain.startLifetime = new ParticleSystem.MinMaxCurve(size * _mainLifeMin * _parScale, size * _mainLifeMax * _parScale); }
        //�X�s�[�h
        if (_mainSpeed == true) { insPSmain.startSpeed = new ParticleSystem.MinMaxCurve(size * _mainSpeedMin * _parScale, size * _mainSpeedMax * _parScale); }
        //�T�C�Y
        if (_mainSize == true) { insPSmain.startSize = new ParticleSystem.MinMaxCurve(size * _mainSizeMin * _parScale, size * _mainSizeMax * _parScale); }
        //�o�[�X�g
        if (_burst == true)
        {
            short burstMinCount = (short)(size * _burstMinCount * _parScale);
            short burstMaxCount = (short)(size * _burstMaxCount * _parScale);
            var newBurst = new ParticleSystem.Burst(0f, burstMinCount, burstMaxCount);// �ݒ���܂Ƃ߂�

            insPS.emission.SetBursts(new ParticleSystem.Burst[0]);// ���݂̃o�[�X�g�ݒ���N���A
            insPS.emission.SetBursts(new ParticleSystem.Burst[] { newBurst });// �V�����o�[�X�g�ݒ��ǉ�
        }
    }

    void Audio(float size, Vector3 pos)
    {
        float myVolume = 0.0f;
        float myPicth = 0.0f;

        GameObject insAud = Instantiate(_objAudio, pos, transform.rotation, _parent.transform); //�C���X�^���X����
        AudioSource audio = insAud.GetComponent<AudioSource>();

        // �{�����[��
        myVolume = (0.5f * size) * _audioVolume;
        audio.volume = Mathf.Clamp(myVolume, 0f, 1f); //���A�����A���
        //Debug.Log("audio.volume: " + audio.volume);

        // �s�b�`
        myPicth = (1.5f - size) * _audioPitch;
        audio.pitch = Mathf.Clamp(myPicth, 0.8f, 1.5f); //�����Ə���͈͓̔��ɐ�������֐�
    }
}