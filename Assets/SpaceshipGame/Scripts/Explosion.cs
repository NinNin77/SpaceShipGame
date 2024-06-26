using System.Drawing;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private float _animScale = 1.0f;
    [SerializeField] private float _sizeWithoutAnim = 0.5f;
    [Header("Particle")]
    [SerializeField] private GameObject _objParticle;
    [SerializeField] private float _parScale = 1.0f;
    [SerializeField] bool _mainLife = true;
    [SerializeField] private float _mainLifeMin = 0.2f;
    [SerializeField] private float _mainLifeMax = 0.8f;
    [SerializeField] bool _mainSpeed = true;
    [SerializeField] private float _mainSpeedMin = 2;
    [SerializeField] private float _mainSpeedMax = 20;
    [SerializeField] bool _mainSize = true;
    [SerializeField] private float _mainSizeMin = 0.01f;
    [SerializeField] private float _mainSizeMax = 0.1f;
    [SerializeField] bool _burst = true;
    [SerializeField] private float _burstMinCount = 15;
    [SerializeField] private float _burstMaxCount = 25;
    [Header("Audio")]
    [SerializeField] private float _audioScale = 1.0f;

    /// <summary>�C���X�^���X�𐶐����A�A�j���[�V�������Đ�����</summary>
    public void Play(float _size, Vector3 _pos)
    {
        Vector3 orgScale = this.transform.localScale;

        // �C���X�^���X ����
        GameObject ins = Instantiate(this.gameObject, _pos, transform.rotation);

        // �A�j���[�V����
        Animator animator = ins.GetComponent<Animator>();
        if (_size < _sizeWithoutAnim) //�ŏ�siza�ȉ���������A�A�j���[�V������\�����Ȃ�
        {
            animator.enabled = false;
        }
        ins.transform.localScale = new Vector3(orgScale[0] * _size * _animScale, orgScale[1] * _size * _animScale, orgScale[2]); //�X�P�[���𒼐ڂ�����

        // ��
        AudioSource audio = ins.GetComponent<AudioSource>();
        audio.volume = Mathf.Clamp(_size * _audioScale, 0f, 1f); //���B�d�g�݂���B�i�j�R���B

        // �p�[�e�B�N��
        GameObject insPar = Instantiate(_objParticle, _pos, transform.rotation);
        ParticleSystem insPS = insPar.GetComponent<ParticleSystem>();
        var insPSmain = insPS.main;
        //�e��ݒ�
        //���C�t�^�C��
        if(_mainLife == true) { insPSmain.startLifetime = new ParticleSystem.MinMaxCurve(_size * _mainLifeMin * _parScale, _size * _mainLifeMax * _parScale); }
        //�X�s�[�h
        if (_mainSpeed == true) { insPSmain.startSpeed = new ParticleSystem.MinMaxCurve(_size * _mainSpeedMin * _parScale, _size * _mainSpeedMax * _parScale); }
        //�T�C�Y
        if (_mainSize == true) { insPSmain.startSize = new ParticleSystem.MinMaxCurve(_size * _mainSizeMin * _parScale, _size * _mainSizeMax * _parScale); }
        //�o�[�X�g
        if (_burst == true)
        {
            short burstMinCount = (short)(_size * _burstMinCount * _parScale);
            short burstMaxCount = (short)(_size * _burstMaxCount * _parScale);
            var newBurst = new ParticleSystem.Burst(0f, burstMinCount, burstMaxCount);// �ݒ���܂Ƃ߂�
            insPS.emission.SetBursts(new ParticleSystem.Burst[0]);// ���݂̃o�[�X�g�ݒ���N���A
            insPS.emission.SetBursts(new ParticleSystem.Burst[] { newBurst });// �V�����o�[�X�g�ݒ��ǉ�
        }
    }
}